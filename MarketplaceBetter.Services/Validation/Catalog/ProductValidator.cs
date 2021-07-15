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

        public ValidationResult Validate(ProductModel brand)
        {
            ValidationResult result = new();

            if (_repository.Any(b => b.Name == brand.Name && b.Id != brand.Id))
            {
                result.AddErrorFor<ProductModel>(b => b.Name, ValidationMessages.NameNotUnique, "Product");
            }

            return result;
        }
    }
}
