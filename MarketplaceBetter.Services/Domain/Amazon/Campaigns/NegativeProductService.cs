using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using Microsofp.EntityFrameworkCore.Storage.ValueConversion;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns
{
    public class NegativeProductService : INegativeProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<NegativeProduct> _repository;
        private readonly IRepository<AdEntityStatus> _adEntityStatusRepository;
        private readonly IRepository<AdGroup> _adGroupRepository;

        public NegativeProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<NegativeProduct>();
            _adEntityStatusRepository = unitOfWork.GetRepository<AdEntityStatus>();
            _adGroupRepository = unitOfWork.GetRepository<AdGroup>();
        }

        public NegativeProductModel Get(long id) => _mapper.Map<NegativeProductModel>(_repository.Get(id));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<NegativeProduct> negativeProducts = _repository.GetQuery();

            ApplyFilter(negativeProducts, request);

            return negativeProducts.Count();
        }

        public IList<NegativeProductModel> GetForListRequest(ListRequest request)
        {
            IQueryable<NegativeProduct> negativeProducts = _repository.GetQuery();

            negativeProducts = ApplyFilter(negativeProducts, request);
            negativeProducts = ApplySorting(negativeProducts, request);
            negativeProducts = ApplyPaging(negativeProducts, request);

            return _mapper.Map<IList<NegativeProductModel>>(negativeProducts);
        }

        public void Add(NegativeProductModel negativeProduct, CampaignModel campaign)
        {
            NegativeProduct negativeProductToAdd = new();

            TransferValues(negativeProductToAdd, negativeProduct);

            negativeProductToAdd.Status = _adEntityStatusRepository.Single(s => s.SystemName == AdEntityStatusEnum.Enabled);

            _repository.Add(negativeProductToAdd);
            _unitOfWork.Save();
        }

        public void Update(NegativeProductModel negativeProduct)
        {
            NegativeProduct negativeProductToUpdate = _repository.Get(negativeProduct.Id);

            TransferValues(negativeProductToUpdate, negativeProduct);

            _repository.Update(negativeProductToUpdate);
            _unitOfWork.Save();
        }

        public void UpdateStatus(long negativeProductId, AdEntityStatusEnum status)
        {
            NegativeProduct negativeProduct = _repository.Get(negativeProductId);
            AdEntityStatus newStatus = _adEntityStatusRepository.Single(s => s.SystemName == status);

            negativeProduct.Status = newStatus;

            _repository.Update(negativeProduct);
            _unitOfWork.Save();
        }

        private void TransferValues(NegativeProduct toNegativeProduct, NegativeProductModel fromNegativeProduct)
        {
            toNegativeProduct.AdGroupId = fromNegativeProduct.AdGroup.Id;
            toNegativeProduct.Asin = fromNegativeProduct.Asin;
            toNegativeProduct.AmazonId = fromNegativeProduct.AmazonId;
        }

        private IQueryable<NegativeProduct> ApplyFilter(IQueryable<NegativeProduct> negativeProducts, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return negativeProducts;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "asin", "matchtype", "amazonid", "status", "campaign" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    negativeProducts = searchField.Name switch
                    {
                        "id" => negativeProducts.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "asin" => negativeProducts.Where(p => p.Asin.Contains(searchField.Value)),
                        "amazonid" => negativeProducts.Where(p => p.AmazonId.Contains(searchField.Value)),
                        "status" => negativeProducts.Where(p => p.Status.Name.Contains(searchField.Value)),
                        "campaign" => negativeProducts.Where(p => p.AdGroup.Campaign.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    negativeProducts = negativeProducts.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Asin.Contains(searchString)
                        || p.AmazonId.Contains(searchString)
                        || p.Status.Name.Contains(searchString)
                        || p.AdGroup.Campaign.Name.Contains(searchString));
                }
            }

            return negativeProducts;
        }

        private IQueryable<NegativeProduct> ApplySorting(IQueryable<NegativeProduct> negativeProducts, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                negativeProducts = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? negativeProducts.OrderBy(p => p.Id) : negativeProducts.OrderByDescending(p => p.Id),
                    "asin" => request.SortDirection == SortDirection.Ascending ? negativeProducts.OrderBy(p => p.Asin) : negativeProducts.OrderByDescending(p => p.Asin),
                    "amazonid" => request.SortDirection == SortDirection.Ascending ? negativeProducts.OrderBy(p => p.AmazonId) : negativeProducts.OrderByDescending(p => p.AmazonId),
                    "status" => request.SortDirection == SortDirection.Ascending ? negativeProducts.OrderBy(p => p.Status.Name) : negativeProducts.OrderByDescending(p => p.Status.Name),
                    "campaign" => request.SortDirection == SortDirection.Ascending ? negativeProducts.OrderBy(p => p.AdGroup.Campaign.Name) : negativeProducts.OrderByDescending(p => p.AdGroup.Campaign.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return negativeProducts;
        }

        private IQueryable<NegativeProduct> ApplyPaging(IQueryable<NegativeProduct> negativeProducts, ListRequest request)
        {
            return negativeProducts.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
