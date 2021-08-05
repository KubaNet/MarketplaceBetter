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
    public class AmazonChildValidator : IAmazonChildValidator
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
                result.AddErrorFor<AmazonChildModel>(p => p.Sku, ValidationMessages.PropertyNotUnique, "Amazon Child", "Sku");
            }

            return result;
        }
    }
}
