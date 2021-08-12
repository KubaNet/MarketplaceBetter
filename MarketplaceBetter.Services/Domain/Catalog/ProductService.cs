using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using MarketplaceBetter.Services.Model.ListRequests.Catalog;
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

        public IList<ProductModel> GetAll() => _mapper.Map<IList<ProductModel>>(_repository.GetAll().OrderBy(g => g.Name));

        public IList<ProductModel> GetForBrand(long brandId) => _mapper.Map<IList<ProductModel>>(_repository.GetQuery().Where(p => p.BrandId == brandId));

        public int CountForListRequest(ProductListRequest request)
        {
            IQueryable<Product> products = _repository.GetQuery();

            ApplyFilter(products, request);

            return products.Count();
        }

        public IList<ProductModel> GetForListRequest(ProductListRequest request)
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
            toProduct.ColorGroupId = fromProduct.ColorGroup.Id;
            toProduct.SizeGroupId = fromProduct.SizeGroup.Id;
        }

        private IQueryable<Product> ApplyFilter(IQueryable<Product> products, ProductListRequest request)
        {
            if (request.Id.HasValue)
            {
                products = products.Where(p => p.Id == request.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                products = products.Where(p => p.Name.Contains(request.Name));
            }

            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                products = products.Where(p => p.Code.Contains(request.Code));
            }

            if (request.Brand != null)
            {
                products = products.Where(p => p.BrandId == request.Brand.Id);
            }

            if (request.ColorGroup != null)
            {
                products = products.Where(p => p.ColorGroupId == request.ColorGroup.Id);
            }

            if (request.SizeGroup != null)
            {
                products = products.Where(p => p.SizeGroupId == request.SizeGroup.Id);
            }

            return products;
        }

        private IQueryable<Product> ApplySorting(IQueryable<Product> products, ProductListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name),
                    "code" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Code) : products.OrderByDescending(p => p.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Brand.Name) : products.OrderByDescending(p => p.Brand.Name),
                    "color_group" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.ColorGroup.Name) : products.OrderByDescending(p => p.ColorGroup.Name),
                    "size_group" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.SizeGroup.Name) : products.OrderByDescending(p => p.SizeGroup.Name),
                    _ => throw new UnrecognizedSortingException<ProductListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<Product> ApplyPaging(IQueryable<Product> products, ProductListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
