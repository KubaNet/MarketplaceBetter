using MarketplaceBetter.Domain.Entities.Amazon.Catalog;
using MarketplaceBetter.Domain.Model.Amazon.Catalog;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Catalog.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Catalog
{
    public class AmazonParentInstanceValidator : IAmazonParentInstanceValidator
    {
        private readonly IRepository<AmazonParentInstance> _repository;

        public AmazonParentInstanceValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AmazonParentInstance>();
        }

        public ValidationResult Validate(AmazonParentInstanceModel parentInstance)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != parentInstance.Id && p.ParentId == parentInstance.Parent.Id 
                && p.InstanceId == parentInstance.Instance.Id))
            {
                result.AddError("There already exists an Amazon Parent Instance for this Parent and Instance.");
            }

            if (_repository.Any(p => p.Id != parentInstance.Id && p.Sku == parentInstance.Sku))
            {
                result.AddErrorFor<AmazonParentInstanceModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Amazon Parent Instance", "SKU");
            }

            if (_repository.Any(p => p.Id != parentInstance.Id && p.Asin != null && p.Asin == parentInstance.Asin))
            {
                result.AddErrorFor<AmazonParentInstanceModel>(p => p.Asin, ValidationMessages.PropertyNotUnique, "Amazon Parent Instance", "ASIN");
            }

            if (parentInstance.Asin != null && parentInstance.Asin.Length != 0 && parentInstance.Asin.Length != 10)
            {
                result.AddErrorFor<AmazonParentInstanceModel>(c => c.Asin, "ASIN should be exactly 10 characters long.");
            }

            return result;
        }
    }
}
