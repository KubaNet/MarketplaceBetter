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

            if (_repository.Any(p => p.Id != parent.Id && p.ProductId == parent.Product.Id 
                && p.InstanceId == parent.Instance.Id))
            {
                result.AddError("There already exists a Parent for this Product and Instance.");
            }

            if (_repository.Any(p => p.Id != parent.Id && p.Sku == parent.Sku))
            {
                result.AddErrorFor<ParentModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Parent", "SKU");
            }

            if (_repository.Any(p => p.Id != parent.Id && p.Asin != null && p.Asin == parent.Asin))
            {
                result.AddErrorFor<ParentModel>(p => p.Asin, ValidationMessages.PropertyNotUnique, "Parent", "ASIN");
            }

            if (parent.Asin != null && parent.Asin.Length != 0 && parent.Asin.Length != 10)
            {
                result.AddErrorFor<ParentModel>(c => c.Asin, "ASIN should be exactly 10 characters long.");
            }

            return result;
        }
    }
}
