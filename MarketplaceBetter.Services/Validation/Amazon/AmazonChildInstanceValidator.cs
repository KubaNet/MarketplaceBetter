using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon
{
    public class AmazonChildInstanceValidator : IAmazonChildInstanceValidator
    {
        private readonly IRepository<AmazonChildInstance> _repository;

        public AmazonChildInstanceValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AmazonChildInstance>();
        }

        public ValidationResult Validate(AmazonChildInstanceModel childInstance)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != childInstance.Id && c.ChildId == childInstance.Child.Id 
                && c.InstanceId == childInstance.Instance.Id))
            {
                result.AddError("There already exists an Amazon Child Instance for this Child and Instance.");
            }

            if (_repository.Any(c => c.Id != childInstance.Id && c.Sku == childInstance.Sku))
            {
                result.AddErrorFor<AmazonChildInstanceModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Amazon Child Instance", "SKU");
            }

            return result;
        }
    }
}
