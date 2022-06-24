using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns
{
    public class ProductAdService : IProductAdService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductAd> _repository;
        private readonly IRepository<Variant> _variantRepository;
        private readonly IRepository<AdEntityStatus> _adEntityStatusRepository;

        public ProductAdService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ProductAd>();
            _variantRepository = unitOfWork.GetRepository<Variant>();
            _adEntityStatusRepository = unitOfWork.GetRepository<AdEntityStatus>();
        }

        public ProductAdModel Get(long id) => _mapper.Map<ProductAdModel>(_repository.Get(id));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ProductAd> productAds = _repository.GetQuery();

            ApplyFilter(productAds, request);

            return productAds.Count();
        }

        public IList<ProductAdModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ProductAd> productAds = _repository.GetQuery();

            productAds = ApplyFilter(productAds, request);
            productAds = ApplySorting(productAds, request);
            productAds = ApplyPaging(productAds, request);

            return _mapper.Map<IList<ProductAdModel>>(productAds);
        }

        public void AddForAdGroup(long adGroupId, long productId)
        {
            IList<Variant> variants = _variantRepository.Where(v => v.ProductId == productId && v.Status.SystemName == VariantStatusEnum.Active).ToList();

            foreach (var variant in variants)
            {
                ProductAd productAd = new ProductAd() { AdGroupId = adGroupId, VariantId = variant.Id, 
                    Status = _adEntityStatusRepository.Single(s => s.SystemName == AdEntityStatusEnum.Enabled) };

                _repository.Add(productAd);
            }

            _unitOfWork.Save();
        }

        public void Update(ProductAdModel productAd)
        {
            ProductAd productAdToUpdate = _repository.Get(productAd.Id);

            TransferValues(productAdToUpdate, productAd);

            _repository.Update(productAdToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(ProductAd toProductAd, ProductAdModel fromProductAd)
        {
            toProductAd.AmazonId = fromProductAd.AmazonId;
        }

        private IQueryable<ProductAd> ApplyFilter(IQueryable<ProductAd> productAds, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return productAds;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "variant", "amazonid", "status", "campaign" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    productAds = searchField.Name switch
                    {
                        "id" => productAds.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "variant" => productAds.Where(p => p.Variant.Sku.Contains(searchField.Value)),
                        "amazonid" => productAds.Where(p => p.AmazonId.Contains(searchField.Value)),
                        "status" => productAds.Where(p => p.Status.Name.Contains(searchField.Value)),
                        "campaign" => productAds.Where(p => p.AdGroup.Campaign.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    productAds = productAds.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Variant.Sku.Contains(searchString)
                        || p.AmazonId.Contains(searchString)
                        || p.Status.Name.Contains(searchString)
                        || p.AdGroup.Campaign.Name.Contains(searchString));
                }
            }

            return productAds;
        }

        private IQueryable<ProductAd> ApplySorting(IQueryable<ProductAd> productAds, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                productAds = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? productAds.OrderBy(p => p.Id) : productAds.OrderByDescending(p => p.Id),
                    "variant" => request.SortDirection == SortDirection.Ascending ? productAds.OrderBy(p => p.Variant.Sku) : productAds.OrderByDescending(p => p.Variant.Sku),
                    "amazonid" => request.SortDirection == SortDirection.Ascending ? productAds.OrderBy(p => p.AmazonId) : productAds.OrderByDescending(p => p.AmazonId),
                    "status" => request.SortDirection == SortDirection.Ascending ? productAds.OrderBy(p => p.Status.Name) : productAds.OrderByDescending(p => p.Status.Name),
                    "campaign" => request.SortDirection == SortDirection.Ascending ? productAds.OrderBy(p => p.AdGroup.Campaign.Name) : productAds.OrderByDescending(p => p.AdGroup.Campaign.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return productAds;
        }

        private IQueryable<ProductAd> ApplyPaging(IQueryable<ProductAd> productAds, ListRequest request)
        {
            return productAds.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
