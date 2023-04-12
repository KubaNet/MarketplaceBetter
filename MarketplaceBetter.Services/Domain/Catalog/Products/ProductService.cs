using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Settings.Interfaces;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _repository;
		private readonly IRepository<EntityStatus> _statusRepository;
        private readonly IParentInstanceService _parentInstanceService;
		private readonly IUserService _userService;

        public ProductService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IParentInstanceService parentInstanceService,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Product>();
			_statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _parentInstanceService = parentInstanceService;
			_userService = userService;
        }

        public ProductModel Get(long id) => _mapper.Map<ProductModel>(_repository.Get(id));

        public IList<ProductModel> GetAll()
        {
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();

                return _mapper.Map<IList<ProductModel>>(_repository.Where(p => p.BrandId == currentBrand.Id).OrderBy(g => g.Name));
            }
            else
            {
                return _mapper.Map<IList<ProductModel>>(_repository.GetQuery().OrderBy(g => g.Name));
            }
        }

        public IList<ProductModel> GetAllForBrand(long brandId) => _mapper.Map<IList<ProductModel>>(_repository.GetQuery().Where(p => p.BrandId == brandId).OrderBy(p => p.Order));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Product> products = _repository.GetQuery();

            products = ApplyFilter(products, request);

            return products.Count();
        }

        public IList<ProductModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Product> products = _repository.GetQuery();

            products = ApplyFilter(products, request);
            products = ApplySorting(products, request);
            products = ApplyPaging(products, request);

            return _mapper.Map<IList<ProductModel>>(products);
        }

        public void Add(ProductModel product)
        {
            Product productToAdd = new();

            TransferValues(productToAdd, product);
            productToAdd.Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft);

            _repository.Add(productToAdd);
            _unitOfWork.Save();

            _parentInstanceService.AddForProduct(productToAdd.Id);
        }

        public void Update(ProductModel product)
        {
            Product productToUpdate = _repository.Get(product.Id);

            TransferValues(productToUpdate, product);

            _repository.Update(productToUpdate);
            _unitOfWork.Save();

            _parentInstanceService.AddForProduct(productToUpdate.Id);
        }

		public int GetMaxOrder()
        {
            if (_repository.GetQuery().Any())
            {
                return _repository.GetQuery().Max(p => p.Order);
            }
            else
            {
                return 0;
            }
        }

        private void TransferValues(Product toProduct, ProductModel fromProduct)
        {
            toProduct.Name = fromProduct.Name;
            toProduct.Code = fromProduct.Code;
            toProduct.BrandId = fromProduct.Brand.Id;
            toProduct.CollectionId = fromProduct.Collection.Id;
            toProduct.ColorGroupId = fromProduct.ColorGroup.Id;
            toProduct.SizeGroupId = fromProduct.SizeGroup.Id;
            toProduct.Order = fromProduct.Order;
        }

        private IQueryable<Product> ApplyFilter(IQueryable<Product> products, ListRequest request)
        {
            if (_userService.IsSpecificBrand())
            {
                products = products.Where(p => p.BrandId == _userService.GetCurrentBrand().Id);
            }

            bool showDrafts = _userService.ShowDrafts();
            if (!showDrafts)
            {
                products = products.Where(p => p.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool showWithdrawn = _userService.ShowWithdrawn();
            if (!showWithdrawn)
            {
                products = products.Where(p => p.Status.SystemName != EntityStatusEnum.Withdrawn);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return products;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "code", "brand", "collection", "color_group", "size_group" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    products = searchField.Name switch
                    {
                        "id" => products.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => products.Where(p => p.Name.Contains(searchField.Value)),
                        "code" => products.Where(p => p.Code.Contains(searchField.Value)),
                        "brand" => products.Where(p => p.Brand.Name.Contains(searchField.Value)),
                        "collection" => products.Where(p => p.Collection.Name.Contains(searchField.Value)),
                        "color_group" => products.Where(p => p.ColorGroup.Name.Contains(searchField.Value)),
                        "size_group" => products.Where(p => p.SizeGroup.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    products = products.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Name.Contains(searchString)
                        || p.Code.Contains(searchString)
                        || p.Brand.Name.Contains(searchString)
                        || p.Collection.Name.Contains(searchString)
                        || p.ColorGroup.Name.Contains(searchString)
                        || p.SizeGroup.Name.Contains(searchString));
                }
            }

            return products;
        }

        private IQueryable<Product> ApplySorting(IQueryable<Product> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name),
                    "code" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Code) : products.OrderByDescending(p => p.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Brand.Name) : products.OrderByDescending(p => p.Brand.Name),
                    "collection" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Collection.Name) : products.OrderByDescending(p => p.Collection.Name),
                    "color_group" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.ColorGroup.Name) : products.OrderByDescending(p => p.ColorGroup.Name),
                    "size_group" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.SizeGroup.Name) : products.OrderByDescending(p => p.SizeGroup.Name),
                    "order" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Order) : products.OrderByDescending(p => p.Order),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                products = products.OrderBy(p => p.Order);
            }

            return products;
        }

        private IQueryable<Product> ApplyPaging(IQueryable<Product> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
