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

            if (_repository.Any(p => p.Id != parent.Id && p.InstanceId == parent.Instance.Id && p.ProductId == parent.Product.Id))
            {
                result.AddError("There already exists an Amazon Parent for this Instance and Product.");
            }

            if (_repository.Any(p => p.Id != parent.Id && p.Sku == parent.Sku))
            {
                result.AddErrorFor<AmazonParentModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Amazon Parent", "SKU");
            }

            if (_repository.Any(p => p.Id != parent.Id && p.Asin != null && p.Asin == parent.Asin))
            {
                result.AddErrorFor<AmazonParentModel>(p => p.Asin, ValidationMessages.PropertyNotUnique, "Amazon Parent", "ASIN");
            }

            return result;
        }
    }
}
