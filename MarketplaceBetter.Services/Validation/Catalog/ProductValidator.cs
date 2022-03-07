using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog
{
    public class ProductValidator : IProductValidator
    {
        private readonly IRepository<Product> _repository;

        public ProductValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Product>();
        }

        public ValidationResult Validate(ProductModel product)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != product.Id && p.Name == product.Name && p.BrandId == product.Brand.Id))
            {
                result.AddErrorFor<ProductModel>(p => p.Name, ValidationMessages.NameNotUniqueForSelected, "Product", "Brand");
            }

            if (_repository.Any(p => p.Id != product.Id && p.Code == product.Code && p.BrandId == product.Brand.Id))
            {
                result.AddErrorFor<ProductModel>(p => p.Code, ValidationMessages.PropertyNotUniqueForSelected, "Product", "Code", "Brand");
            }

            return result;
        }
    }
}
