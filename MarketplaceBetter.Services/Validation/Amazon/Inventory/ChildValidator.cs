using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;

namespace MarketplaceBetter.Services.Validation.Amazon.Inventory
{
    public class ChildValidator : Interfaces.IChildValidator
    {
        private readonly IRepository<Child> _repository;

        public ChildValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Child>();
        }

        public ValidationResult Validate(ChildModel child)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != child.Id && c.ParentId == child.Parent.Id && c.VariantId == child.Variant.Id))
            {
                result.AddError("There already exists a Child for this Parent and Variant.");
            }

            if (_repository.Any(c => c.Id != child.Id && c.Sku == child.Sku))
            {
                result.AddErrorFor<ChildModel>(c => c.Sku, ValidationMessages.PropertyNotUnique, "Child", "Sku");
            }

            if (child.Asin != null && child.Asin.Length != 0 && child.Asin.Length != 10)
            {
                result.AddErrorFor<ChildModel>(c => c.Asin, "ASIN should be exactly 10 characters long.");
            }

            return result;
        }
    }
}
