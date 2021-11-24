using AutoMapper;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon
{
    public class AmazonTargetingService : IAmazonTargetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonTargeting> _repository;
        private readonly IMapper _mapper;

        public AmazonTargetingService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonTargeting>();
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

        public void Add(AmazonTargetingModel targeting)
        {
            AmazonTargeting targetingToAdd = new();

            TransferValues(targetingToAdd, targeting);

            _repository.Add(targetingToAdd);
            _unitOfWork.Save();
        }

        public void Update(AmazonTargetingModel targeting)
        {
            AmazonTargeting targetingToUpdate = _repository.Get(targeting.Id);

            TransferValues(targetingToUpdate, targeting);

            _repository.Update(targetingToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(AmazonTargeting toTargeting, AmazonTargetingModel fromTargeting)
        {
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
                string[] searchFieldNames = new[] { "id", "value", "campaign", "type", "status"};
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
