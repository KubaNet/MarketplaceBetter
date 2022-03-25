using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Inventory.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Inventory
{
    public class ChildInstanceValidator : IChildInstanceValidator
    {
        private readonly IRepository<ChildInstance> _repository;

        public ChildInstanceValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ChildInstance>();
        }

        public ValidationResult Validate(ChildInstanceModel childInstance)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != childInstance.Id && c.ChildId == childInstance.Child.Id 
                && c.InstanceId == childInstance.Instance.Id))
            {
                result.AddError("There already exists an Child Instance for this Child and Instance.");
            }

            if (_repository.Any(c => c.Id != childInstance.Id && c.Sku == childInstance.Sku))
            {
                result.AddErrorFor<ChildInstanceModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Child Instance", "SKU");
            }

            return result;
        }
    }
}
