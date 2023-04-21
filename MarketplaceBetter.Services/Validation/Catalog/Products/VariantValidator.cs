using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.Products
{
    public class VariantValidator : IVariantValidator
    {
        private readonly IRepository<Variant> _repository;

        public VariantValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Variant>();
        }

        public ValidationResult Validate(VariantModel variant)
        {
            ValidationResult result = new();

            if (_repository.Any(v => v.Id != variant.Id && v.Sku == variant.Sku))
            {
                result.AddErrorFor<VariantModel>(v => v.Sku, ValidationMessages.PropertyNotUnique, "Variant", "SKU");
            }

            if (_repository.Any(v => v.Id != variant.Id && v.Asin != null && v.Asin == variant.Asin))
            {
                result.AddErrorFor<VariantModel>(v => v.Asin, ValidationMessages.PropertyNotUnique, "Variant", "ASIN");
            }

            if (variant.Asin != null && variant.Asin.Length != 0 && variant.Asin.Length != 10)
            {
                result.AddErrorFor<VariantModel>(v => v.Asin, "ASIN should be exactly 10 characters long.");
            }

            if (_repository.Any(v => v.Id != variant.Id && v.ProductId == variant.Product.Id && v.ColorId == variant.Color.Id && v.SizeId == variant.Size.Id))
            {
                result.AddError("There already exists such a variant.");
            }

            return result;
        }
    }
}
