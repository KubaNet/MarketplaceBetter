using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon
{
    public class AmazonTargetingService : IAmazonTargetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonTargeting> _repository;
        private readonly IRepository<AmazonTargetingStatus> _statusRepository;
        private readonly IRepository<AmazonTargetingType> _typeRepository;
        private readonly IMapper _mapper;

        public AmazonTargetingService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonTargeting>();
            _statusRepository = unitOfWork.GetRepository<AmazonTargetingStatus>();
            _typeRepository = unitOfWork.GetRepository<AmazonTargetingType>();
            _mapper = mapper;
        }

        public AmazonTargetingModel Get(long id) => _mapper.Map<AmazonTargetingModel>(_repository.Get(id));

        public IList<AmazonTargetingModel> GetAll() => _mapper.Map<IList<AmazonTargetingModel>>(_repository.GetQuery());

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<AmazonTargeting> targeting = _repository.GetQuery();

            ApplyFilter(targeting, request);

            return targeting.Count();
        }

        public IList<AmazonTargetingModel> GetForListRequest(ListRequest request)
        {
            IQueryable<AmazonTargeting> targeting = _repository.GetQuery();

            targeting = ApplyFilter(targeting, request);
            targeting = ApplySorting(targeting, request);
            targeting = ApplyPaging(targeting, request);

            return _mapper.Map<IList<AmazonTargetingModel>>(targeting);
        }

        public void Add(AmazonTargetingModel targeting, Stream file)
        {
            ReadAndSaveTargeting(targeting.Campaign, file);
        }

        public void SetAsNegative(IList<AmazonTargetingModel> targeting)
        {
            foreach (var singleTargeting in targeting)
            {
                AmazonTargeting amzTargeting = _repository.Get(singleTargeting.Id);

                amzTargeting.Status = _statusRepository.Single(s => s.SystemName == AmazonTargetingStatusEnum.Negative);

                _repository.Update(amzTargeting);
            }

            _unitOfWork.Save();
        }

        public void SetAsActive(IList<AmazonTargetingModel> targeting)
        {
            foreach (var singleTargeting in targeting)
            {
                AmazonTargeting amzTargeting = _repository.Get(singleTargeting.Id);

                amzTargeting.Status = _statusRepository.Single(s => s.SystemName == AmazonTargetingStatusEnum.Active);

                _repository.Update(amzTargeting);
            }

            _unitOfWork.Save();
        }

        private void ReadAndSaveTargeting(AmazonCampaignModel campaign, Stream file)
        {
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
            };

            using var reader = new StreamReader(file);
            using var csv = new CsvReader(reader, config);

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                string targeting = csv.GetField(0);

                ProcessTargeting(campaign, targeting);
            }
        }

        private void ProcessTargeting(AmazonCampaignModel campaign, string targetingValue)
        {
            AmazonTargeting targeting = _repository.SingleOrDefault(t => t.CampaignId == campaign.Id && t.Value == targetingValue);
            if (targeting != null)
            {
                return;
            }

            targeting = new AmazonTargeting
            {
                CampaignId = campaign.Id,
                Value = targetingValue,
                Status = _statusRepository.Single(s => s.SystemName == AmazonTargetingStatusEnum.New),
                Type = targetingValue.StartsWith("b0") ? _typeRepository.Single(t => t.SystemName == AmazonTargetingTypeEnum.Product) : _typeRepository.Single(t => t.SystemName == AmazonTargetingTypeEnum.Keyword)
            };

            _repository.Add(targeting);
            _unitOfWork.Save();
        }

        private IQueryable<AmazonTargeting> ApplyFilter(IQueryable<AmazonTargeting> products, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return products;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "value", "campaign", "type", "status" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    products = searchField.Name switch
                    {
                        "id" => products.Where(t => t.Id == searchField.Value.ParseToIntOrDefault()),
                        "value" => products.Where(t => t.Value.Contains(searchField.Value)),
                        "campaign" => products.Where(t => t.Campaign.Name.Contains(searchField.Value)),
                        "type" => products.Where(t => t.Type.Name.Contains(searchField.Value)),
                        "status" => products.Where(t => t.Status.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    products = products.Where(t => t.Id == searchString.ParseToIntOrDefault()
                        || t.Value.Contains(searchString)
                        || t.Campaign.Name.Contains(searchString)
                        || t.Type.Name.Contains(searchString)
                        || t.Status.Name.Contains(searchString));
                }
            }

            return products;
        }

        private IQueryable<AmazonTargeting> ApplySorting(IQueryable<AmazonTargeting> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(t => t.Id) : products.OrderByDescending(t => t.Id),
                    "value" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(t => t.Value) : products.OrderByDescending(t => t.Value),
                    "campaign" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(t => t.Campaign.Name) : products.OrderByDescending(t => t.Campaign.Name),
                    "type" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(t => t.Type.Name) : products.OrderByDescending(t => t.Type.Name),
                    "status" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(t => t.Status.Name) : products.OrderByDescending(t => t.Status.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<AmazonTargeting> ApplyPaging(IQueryable<AmazonTargeting> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
