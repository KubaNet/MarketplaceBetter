using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
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
            Product brand = _repository.Get(id);

            return _mapper.Map<ProductModel>(brand);
        }

        public IList<ProductModel> GetAll()
        {
            IList<ProductModel> brands = _mapper.Map<IList<ProductModel>>(_repository.GetAll());

            return brands;
        }

        public void Add(ProductModel brand)
        {
            Product brandToAdd = new();

            TransferValues(brandToAdd, brand);

            _repository.Add(brandToAdd);
            _unitOfWork.Save();
        }

        public void Update(ProductModel brand)
        {
            Product brandToUpdate = _repository.Get(brand.Id);

            TransferValues(brandToUpdate, brand);

            _repository.Update(brandToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Product toProduct, ProductModel fromProduct)
        {
            toProduct.Name = fromProduct.Name;
            toProduct.BrandId = fromProduct.BrandId;
        }
    }
}
