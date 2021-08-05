using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            Product product = _repository.Get(id);

            return _mapper.Map<ProductModel>(product);
        }

        public IList<ProductModel> GetAll()
        {
            IList<ProductModel> products = _mapper.Map<IList<ProductModel>>(_repository.GetAll());

            return products;
        }

        public IList<ProductModel> GetAllForBrand(long brandId)
        {
            IList<ProductModel> products = _mapper.Map<IList<ProductModel>>(_repository.GetQuery().Where(p => p.BrandId == brandId));

            return products;
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
