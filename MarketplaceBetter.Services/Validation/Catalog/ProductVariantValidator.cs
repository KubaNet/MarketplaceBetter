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
    public class ProductVariantValidator : IProductVariantValidator
    {
        private readonly IRepository<ProductVariant> _repository;

        public ProductVariantValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ProductVariant>();
        }

        public ValidationResult Validate(ProductVariantModel variant)
        {
            ValidationResult result = new();

            if (_repository.Any(v => v.Id != variant.Id && v.Sku == variant.Sku))
            {
                result.AddErrorFor<ProductVariantModel>(v => v.Sku, ValidationMessages.PropertyNotUnique, "Product Variant", "Sku");
            }

            return result;
        }
    }
}
