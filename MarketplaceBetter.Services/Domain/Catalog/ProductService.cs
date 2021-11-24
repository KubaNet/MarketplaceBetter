using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Product>();
            _mapper = mapper;
        }

        public ProductModel Get(long id) => _mapper.Map<ProductModel>(_repository.Get(id));

        public IList<ProductModel> GetAll() => _mapper.Map<IList<ProductModel>>(_repository.GetQuery().OrderBy(g => g.Name));

        public IList<ProductModel> GetForBrand(long brandId) => _mapper.Map<IList<ProductModel>>(_repository.GetQuery().Where(p => p.BrandId == brandId).OrderBy(p => p.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Product> products = _repository.GetQuery();

            ApplyFilter(products, request);

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

            _repository.Add(productToAdd);
            _unitOfWork.Save();
        }

        public void Update(ProductModel product)
        {
            Product productToUpdate = _repository.Get(product.Id);

            TransferValues(productToUpdate, product);

            _repository.Update(productToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Product toProduct, ProductModel fromProduct)
        {
            toProduct.Name = fromProduct.Name;
            toProduct.Code = fromProduct.Code;
            toProduct.BrandId = fromProduct.Brand.Id;
            toProduct.CollectionId = fromProduct.Collection.Id;
            toProduct.ColorGroupId = fromProduct.ColorGroup.Id;
            toProduct.SizeGroupId = fromProduct.SizeGroup.Id;
        }

        private IQueryable<Product> ApplyFilter(IQueryable<Product> products, ListRequest request)
        {
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
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<Product> ApplyPaging(IQueryable<Product> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
