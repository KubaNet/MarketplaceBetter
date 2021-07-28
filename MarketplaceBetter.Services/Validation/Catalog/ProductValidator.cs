using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
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

            if (_repository.Any(p => p.Name == product.Name && product.Brand.Id == p.BrandId && p.Id != product.Id))
            {
                result.AddErrorFor<ProductModel>(p => p.Name, ValidationMessages.NameNotUnique, "Product");
            }

            if (_repository.Any(p => p.Code == product.Code && product.Brand.Id == p.BrandId && p.Id != product.Id))
            {
                result.AddErrorFor<ProductModel>(p => p.Code, ValidationMessages.PropertyNotUnique, "Product", "Code");
            }

            return result;
        }
    }
}
