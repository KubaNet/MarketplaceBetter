using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.APlusContents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.APlusContents
{
	public class APlusContentValidator : IAPlusContentValidator
	{
        private readonly IRepository<APlusContent> _repository;

        public APlusContentValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<APlusContent>();
        }

        public ValidationResult Validate(APlusContentModel content, bool hasSelectedVariants)
        {
            ValidationResult result = new ValidationResult();

            if (_repository.Any(c => c.Id != content.Id && c.Name == content.Name))
            {
                result.AddErrorFor<APlusContentModel>(c => c.Name, ValidationMessages.NameNotUnique, "A+ Content");
            }

            if (!content.AllVariants && !hasSelectedVariants)
            {
                result.AddError("Select variants or click \"All variants\" checkbox.");
            }

            return result;
		}
	}
}
