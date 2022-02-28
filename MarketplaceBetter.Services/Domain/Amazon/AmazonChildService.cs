using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.Model.Catalog;
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
    public class AmazonChildService : IAmazonChildService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonChild> _repository;
        private readonly IRepository<AmazonParent> _parentRepository;
        private readonly IRepository<ProductVariant> _productVariantRepository;
        private readonly IAmazonChildInstanceService _childInstanceService;

        public AmazonChildService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IAmazonChildInstanceService childInstanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonChild>();
            _parentRepository = unitOfWork.GetRepository<AmazonParent>();
            _productVariantRepository = unitOfWork.GetRepository<ProductVariant>();
            _childInstanceService = childInstanceService;
        }

        public AmazonChildModel Get(long id) => _mapper.Map<AmazonChildModel>(_repository.Get(id));

        public IList<AmazonChildModel> GetAll() => _mapper.Map<IList<AmazonChildModel>>(_repository.GetQuery().OrderBy(c => c.Sku));

        public IList<AmazonChildModel> GetAllForParent(long parentId) => _mapper.Map<IList<AmazonChildModel>>(_repository.GetQuery().Where(c => c.ParentId == parentId).OrderBy(c => c.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<AmazonChild> childs = _repository.GetQuery();

            ApplyFilter(childs, request);

            return childs.Count();
        }

        public IList<AmazonChildModel> GetForListRequest(ListRequest request)
        {
            IQueryable<AmazonChild> childs = _repository.GetQuery();

            childs = ApplyFilter(childs, request);
            childs = ApplySorting(childs, request);
            childs = ApplyPaging(childs, request);

            return _mapper.Map<IList<AmazonChildModel>>(childs);
        }

        public void Add(AmazonChildModel child)
        {
            AmazonChild childToAdd = new();

            TransferValues(childToAdd, child);

            _repository.Add(childToAdd);
            _unitOfWork.Save();

            _childInstanceService.AddForChild(childToAdd.Id);
        }

        public void AddForParent(long parentId)
        {
            AmazonParent parent = _parentRepository.Get(parentId);

            IList<ProductVariant> productVariants = _productVariantRepository.GetQuery().Where(v => v.ProductId == parent.ProductId).ToList();
            foreach (var productVariant in productVariants)
            {
                if (_repository.Any(c => c.ParentId == parentId && c.ProductVariantId == productVariant.Id))
                {
                    continue;
                }

                AmazonChild child = new AmazonChild { ParentId = parentId, ProductVariantId = productVariant.Id, 
                    Sku = GetSkuFor(parentId, productVariant.Id) };

                _repository.Add(child);
                _unitOfWork.Save();

                _childInstanceService.AddForChild(child.Id);
            }
        }

        public void AddForProductVariant(long productVariantId)
        {
            ProductVariant productVariant = _productVariantRepository.Get(productVariantId);
            AmazonParent parent = _parentRepository.SingleOrDefault(p => p.ProductId == productVariant.ProductId);

            if (parent != null)
            {
                AmazonChild child = _repository.SingleOrDefault(c => c.ParentId == parent.Id 
                    && c.ProductVariantId == productVariantId);

                if (child != null)
                {
                    return;
                }

                child = new AmazonChild { ParentId = parent.Id, ProductVariantId = productVariantId,
                    Sku = GetSkuFor(parent.Id, productVariantId) };

                _repository.Add(child);
                _unitOfWork.Save();

                _childInstanceService.AddForChild(child.Id);
            }
        }

        public void Update(AmazonChildModel child)
        {
            AmazonChild childToUpdate = _repository.Get(child.Id);

            TransferValues(childToUpdate, child);

            _repository.Update(childToUpdate);
            _unitOfWork.Save();

            _childInstanceService.AddForChild(childToUpdate.Id);
        }

        public string GetSkuFor(long? parentId, long? productVariantId)
        {
            if (parentId.HasValue && productVariantId.HasValue)
            {
                AmazonParent parent = _parentRepository.Get(parentId.Value);
                ProductVariant productVariant = _productVariantRepository.Get(productVariantId.Value);

                return $"{parent.ChildSku}_{productVariant.Color.Code}_{productVariant.Size.Code}";
            }
            else if (parentId.HasValue)
            {
                AmazonParent parent = _parentRepository.Get(parentId.Value);

                return $"{parent.ChildSku}";
            }

            return null;
        }

        private void TransferValues(AmazonChild toChild, AmazonChildModel fromChild)
        {
            toChild.ParentId = fromChild.Parent.Id;
            toChild.ProductVariantId = fromChild.ProductVariant.Id;
            toChild.Sku = fromChild.Sku;
            toChild.Asin = fromChild.Asin;
        }

        private IQueryable<AmazonChild> ApplyFilter(IQueryable<AmazonChild> childs, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return childs;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "asin", "product_variant", "parent", "brand" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    childs = searchField.Name switch
                    {
                        "id" => childs.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => childs.Where(c => c.Sku.Contains(searchField.Value)),
                        "asin" => childs.Where(c => c.Asin.Contains(searchField.Value)),
                        "product_variant" => childs.Where(c => c.ProductVariant.Sku.Contains(searchField.Value)),
                        "parent" => childs.Where(c => c.Parent.Sku.Contains(searchField.Value)),
                        "brand" => childs.Where(c => c.Parent.Product.Brand.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    childs = childs.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Sku.Contains(searchString)
                        || c.Asin.Contains(searchString)
                        || c.ProductVariant.Sku.Contains(searchString)
                        || c.Parent.Sku.Contains(searchString)
                        || c.Parent.Product.Brand.Name.Contains(searchString));
                }
            }

            return childs;
        }

        private IQueryable<AmazonChild> ApplySorting(IQueryable<AmazonChild> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Id) : products.OrderByDescending(c => c.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Sku) : products.OrderByDescending(c => c.Sku),
                    "asin" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Asin) : products.OrderByDescending(c => c.Asin),
                    "product_variant" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.ProductVariant.Sku) : products.OrderByDescending(c => c.ProductVariant.Sku),
                    "parent" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Parent.Sku) : products.OrderByDescending(c => c.Parent.Sku),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Parent.Product.Brand.Name) : products.OrderByDescending(c => c.Parent.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<AmazonChild> ApplyPaging(IQueryable<AmazonChild> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
