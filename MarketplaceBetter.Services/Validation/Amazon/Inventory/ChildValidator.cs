using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Inventory.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Inventory
{
    public class ChildValidator : IChildValidator
    {
        private readonly IRepository<Child> _repository;

        public ChildValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Child>();
        }

        public ValidationResult Validate(ChildModel child)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != child.Id && c.VariantId == child.Variant.Id 
                && c.InstanceId == child.Instance.Id))
            {
                result.AddError("There already exists a Child for this Variant and Instance.");
            }

            if (_repository.Any(c => c.Id != child.Id && c.Sku == child.Sku))
            {
                result.AddErrorFor<ChildModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Child", "SKU");
            }

            return result;
        }
    }
}
