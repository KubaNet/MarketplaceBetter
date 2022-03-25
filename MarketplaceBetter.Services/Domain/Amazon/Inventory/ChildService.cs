using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class ChildService : IChildService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Child> _repository;
        private readonly IRepository<Parent> _parentRepository;
        private readonly IRepository<MarketplaceBetter.Domain.Entities.Catalog.Products.Variant> _VariantRepository;
        private readonly IChildInstanceService _childInstanceService;

        public ChildService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IChildInstanceService childInstanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Child>();
            _parentRepository = unitOfWork.GetRepository<Parent>();
            _VariantRepository = unitOfWork.GetRepository<MarketplaceBetter.Domain.Entities.Catalog.Products.Variant>();
            _childInstanceService = childInstanceService;
        }

        public ChildModel Get(long id) => _mapper.Map<ChildModel>(_repository.Get(id));

        public IList<ChildModel> GetAll() => _mapper.Map<IList<ChildModel>>(_repository.GetQuery().OrderBy(c => c.Sku));

        public IList<ChildModel> GetAllForParent(long parentId) => _mapper.Map<IList<ChildModel>>(_repository.GetQuery().Where(c => c.ParentId == parentId).OrderBy(c => c.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Child> childs = _repository.GetQuery();

            ApplyFilter(childs, request);

            return childs.Count();
        }

        public IList<ChildModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Child> childs = _repository.GetQuery();

            childs = ApplyFilter(childs, request);
            childs = ApplySorting(childs, request);
            childs = ApplyPaging(childs, request);

            return _mapper.Map<IList<ChildModel>>(childs);
        }

        public void Add(ChildModel child)
        {
            Child childToAdd = new();

            TransferValues(childToAdd, child);

            _repository.Add(childToAdd);
            _unitOfWork.Save();

            _childInstanceService.AddForChild(childToAdd.Id);
        }

        public void AddForParent(long parentId)
        {
            Parent parent = _parentRepository.Get(parentId);

            IList<MarketplaceBetter.Domain.Entities.Catalog.Products.Variant> Variants = _VariantRepository.GetQuery().Where(v => v.ProductId == parent.ProductId).ToList();
            foreach (var Variant in Variants)
            {
                if (_repository.Any(c => c.ParentId == parentId && c.VariantId == Variant.Id))
                {
                    continue;
                }

                Child child = new Child { ParentId = parentId, VariantId = Variant.Id, 
                    Sku = GetSkuFor(parentId, Variant.Id) };

                _repository.Add(child);
                _unitOfWork.Save();

                _childInstanceService.AddForChild(child.Id);
            }
        }

        public void AddForVariant(long VariantId)
        {
            MarketplaceBetter.Domain.Entities.Catalog.Products.Variant Variant = _VariantRepository.Get(VariantId);
            Parent parent = _parentRepository.SingleOrDefault(p => p.ProductId == Variant.ProductId);

            if (parent != null)
            {
                Child child = _repository.SingleOrDefault(c => c.ParentId == parent.Id 
                    && c.VariantId == VariantId);

                if (child != null)
                {
                    return;
                }

                child = new Child { ParentId = parent.Id, VariantId = VariantId,
                    Sku = GetSkuFor(parent.Id, VariantId) };

                _repository.Add(child);
                _unitOfWork.Save();

                _childInstanceService.AddForChild(child.Id);
            }
        }

        public void Update(ChildModel child)
        {
            Child childToUpdate = _repository.Get(child.Id);

            TransferValues(childToUpdate, child);

            _repository.Update(childToUpdate);
            _unitOfWork.Save();

            _childInstanceService.AddForChild(childToUpdate.Id);
        }

        public string GetSkuFor(long? parentId, long? VariantId)
        {
            if (parentId.HasValue && VariantId.HasValue)
            {
                Parent parent = _parentRepository.Get(parentId.Value);
                MarketplaceBetter.Domain.Entities.Catalog.Products.Variant Variant = _VariantRepository.Get(VariantId.Value);

                if (Variant.Size.IsOneSize)
                {
                    return $"{parent.ChildSku}_{Variant.Color.Code}";
                }
                else
                {
                    return $"{parent.ChildSku}_{Variant.Color.Code}_{Variant.Size.Code}";
                }
            }
            else if (parentId.HasValue)
            {
                Parent parent = _parentRepository.Get(parentId.Value);

                return $"{parent.ChildSku}";
            }

            return null;
        }

        private void TransferValues(Child toChild, ChildModel fromChild)
        {
            toChild.ParentId = fromChild.Parent.Id;
            toChild.VariantId = fromChild.Variant.Id;
            toChild.Sku = fromChild.Sku;
            toChild.Asin = fromChild.Asin;
        }

        private IQueryable<Child> ApplyFilter(IQueryable<Child> childs, ListRequest request)
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
                        "product_variant" => childs.Where(c => c.Variant.Sku.Contains(searchField.Value)),
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
                        || c.Variant.Sku.Contains(searchString)
                        || c.Parent.Sku.Contains(searchString)
                        || c.Parent.Product.Brand.Name.Contains(searchString));
                }
            }

            return childs;
        }

        private IQueryable<Child> ApplySorting(IQueryable<Child> childs, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                childs = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Id) : childs.OrderByDescending(c => c.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Sku) : childs.OrderByDescending(c => c.Sku),
                    "asin" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Asin) : childs.OrderByDescending(c => c.Asin),
                    "product_variant" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Variant.Sku) : childs.OrderByDescending(c => c.Variant.Sku),
                    "parent" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Parent.Sku) : childs.OrderByDescending(c => c.Parent.Sku),
                    "brand" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Parent.Product.Brand.Name) : childs.OrderByDescending(c => c.Parent.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return childs;
        }

        private IQueryable<Child> ApplyPaging(IQueryable<Child> childs, ListRequest request)
        {
            return childs.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
