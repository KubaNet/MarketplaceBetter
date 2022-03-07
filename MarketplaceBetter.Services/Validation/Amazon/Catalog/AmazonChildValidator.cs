using MarketplaceBetter.Domain.Entities.Amazon.Catalog;
using MarketplaceBetter.Domain.Model.Amazon.Catalog;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;

namespace MarketplaceBetter.Services.Validation.Amazon.Catalog
{
    public class AmazonChildValidator : Interfaces.IAmazonChildValidator
    {
        private readonly IRepository<AmazonChild> _repository;

        public AmazonChildValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AmazonChild>();
        }

        public ValidationResult Validate(AmazonChildModel child)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != child.Id && c.ParentId == child.Parent.Id && c.ProductVariantId == child.ProductVariant.Id))
            {
                result.AddError("There already exists an Amazon Child for this Parent and Product Variant.");
            }

            if (_repository.Any(c => c.Id != child.Id && c.Sku == child.Sku))
            {
                result.AddErrorFor<AmazonChildModel>(c => c.Sku, ValidationMessages.PropertyNotUnique, "Amazon Child", "Sku");
            }

            if (child.Asin != null && child.Asin.Length != 0 && child.Asin.Length != 10)
            {
                result.AddErrorFor<AmazonChildModel>(c => c.Asin, "ASIN should be exactly 10 characters long.");
            }

            return result;
        }
    }
}
