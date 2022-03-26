using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Inventory.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Inventory
{
    public class ParentValidator : IParentValidator
    {
        private readonly IRepository<Parent> _repository;

        public ParentValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Parent>();
        }

        public ValidationResult Validate(ParentModel parent)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != parent.Id && p.ProductId == parent.Product.Id))
            {
                result.AddError("There already exists an Parent for this Product.");
            }

            if (_repository.Any(p => p.Id != parent.Id && p.Sku == parent.Sku))
            {
                result.AddErrorFor<ParentModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Parent", "SKU");
            }

            return result;
        }
    }
}
