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

        public ProductModel Get(long id)
        {
            return _mapper.Map<ProductModel>(_repository.Get(id));
        }

        public IList<ProductModel> GetAll()
        {
            return _mapper.Map<IList<ProductModel>>(_repository.GetAll());
        }

        public IList<ProductModel> GetForBrand(long brandId)
        {
            return _mapper.Map<IList<ProductModel>>(_repository.GetQuery().Where(p => p.BrandId == brandId));
        }

        public IList<ProductModel> GetForListRequest(ProductListRequest request)
        {
            IQueryable<Product> products = _repository.GetQuery();

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

            products = products.Skip(request.Page * request.PageSize).Take(request.PageSize);

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
    }
}
