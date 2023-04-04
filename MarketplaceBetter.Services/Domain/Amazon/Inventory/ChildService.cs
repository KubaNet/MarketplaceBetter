using AutoMapper;
using MarketplaceBetter.Domain.Constants;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class ChildService : IChildService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Child> _repository;
        private readonly IRepository<Parent> _parentRepository;
        private readonly IRepository<Variant> _variantRepository;
        private readonly IChildInstanceService _childInstanceService;
        private readonly ICurrentBrandService _currentBrandService;

        public ChildService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IChildInstanceService childInstanceService,
            ICurrentBrandService currentBrandService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Child>();
            _parentRepository = unitOfWork.GetRepository<Parent>();
            _variantRepository = unitOfWork.GetRepository<Variant>();
            _childInstanceService = childInstanceService;
            _currentBrandService = currentBrandService;
        }

        public ChildModel Get(long id) => _mapper.Map<ChildModel>(_repository.Get(id));

        public IList<ChildModel> GetAll() => _mapper.Map<IList<ChildModel>>(_repository.GetQuery().OrderBy(c => c.Sku));

        public IList<ChildModel> GetAllForParent(long parentId) => _mapper.Map<IList<ChildModel>>(_repository.GetQuery().Where(c => c.ParentId == parentId).OrderBy(c => c.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Child> childs = _repository.GetQuery();

            childs = ApplyFilter(childs, request);

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

            IList<Variant> Variants = _variantRepository.GetQuery().Where(v => v.ProductId == parent.ProductId).ToList();
            foreach (var Variant in Variants)
            {
                if (_repository.Any(c => c.ParentId == parentId && c.VariantId == Variant.Id))
                {
                    continue;
                }

                Child child = new Child { ParentId = parentId, VariantId = Variant.Id, Sku = GetSkuFor(Variant.Id) };

                _repository.Add(child);
                _unitOfWork.Save();

                _childInstanceService.AddForChild(child.Id);
            }
        }

        public void AddForVariant(long VariantId)
        {
            Variant Variant = _variantRepository.Get(VariantId);
            Parent parent = _parentRepository.SingleOrDefault(p => p.ProductId == Variant.ProductId);

            if (parent != null)
            {
                Child child = _repository.SingleOrDefault(c => c.ParentId == parent.Id 
                    && c.VariantId == VariantId);

                if (child != null)
                {
                    return;
                }

                child = new Child { ParentId = parent.Id, VariantId = VariantId, Sku = GetSkuFor(VariantId) };

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

        public string GetSkuFor(long? VariantId)
        {
            if (VariantId.HasValue)
            {
                Variant Variant = _variantRepository.Get(VariantId.Value);

                return Variant.Sku;
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
            if (_currentBrandService.IsSpecificBrand())
            {
                childs = childs.Where(c => c.Parent.Product.BrandId == _currentBrandService.GetCurrentBrand().Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return childs;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "asin", "variant", "parent", "brand", "product_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    childs = searchField.Name switch
                    {
                        "id" => childs.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => childs.Where(c => c.Sku.Contains(searchField.Value)),
                        "asin" => childs.Where(c => c.Asin.Contains(searchField.Value)),
                        "variant" => childs.Where(c => c.Variant.Sku.Contains(searchField.Value)),
                        "parent" => childs.Where(c => c.Parent.Sku.Contains(searchField.Value)),
                        "brand" => childs.Where(c => c.Parent.Product.Brand.Name.Contains(searchField.Value)),
                        "product_id" => childs.Where(c => c.Parent.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    if (searchString.Contains("TooLong", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    childs = childs.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Sku.Contains(searchString)
                        || c.Asin.Contains(searchString)
                        || c.Variant.Sku.Contains(searchString)
                        || c.Parent.Sku.Contains(searchString)
                        || c.Parent.Product.Brand.Name.Contains(searchString));
                }
            }

            if (searchStrings.Any(s => s.Contains("TooLong", StringComparison.OrdinalIgnoreCase)))
            {
                childs = childs.Where(c => c.Sku.Length > Standard.AmazonSkuMaxLength - 4);
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
                    "variant" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Variant.Sku) : childs.OrderByDescending(c => c.Variant.Sku),
                    "parent" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Parent.Sku) : childs.OrderByDescending(c => c.Parent.Sku),
                    "brand" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Parent.Product.Brand.Name) : childs.OrderByDescending(c => c.Parent.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return childs;
        }

        private IQueryable<Child> ApplyPaging(IQueryable<Child> childs, ListRequest request)
        {
            return request.ShowAll ? childs : childs.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
