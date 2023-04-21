using MarketplaceBetter.Domain.Entities.Catalog.Attributes;
using MarketplaceBetter.Domain.Model.Catalog.Attributes;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.Attributes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.Attributes
{
    public class ProductDimensionsValidator : IProductDimensionsValidator
    {
        private readonly IRepository<ProductDimensions> _repository;

        public ProductDimensionsValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ProductDimensions>();
        }

        public ValidationResult Validate(ProductDimensionsModel dimensions)
        {
            ValidationResult result = new();

            if (_repository.Any(v => v.Id != dimensions.Id && v.ProductId == dimensions.Product.Id && v.SizeId == dimensions.Size.Id))
            {
                result.AddError("There already exists such a product dimensions.");
            }

            return result;
        }
    }
}
