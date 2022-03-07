using MarketplaceBetter.Domain.Entities.Amazon.Catalog;
using MarketplaceBetter.Domain.Model.Amazon.Catalog;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Catalog.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Catalog
{
    public class AmazonParentValidator : IAmazonParentValidator
    {
        private readonly IRepository<AmazonParent> _repository;

        public AmazonParentValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AmazonParent>();
        }

        public ValidationResult Validate(AmazonParentModel parent)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != parent.Id && p.ProductId == parent.Product.Id))
            {
                result.AddError("There already exists an Amazon Parent for this Product.");
            }

            if (_repository.Any(p => p.Id != parent.Id && p.Sku == parent.Sku))
            {
                result.AddErrorFor<AmazonParentModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Amazon Parent", "SKU");
            }

            return result;
        }
    }
}
