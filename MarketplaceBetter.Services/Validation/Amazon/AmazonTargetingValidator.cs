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
    public class AmazonTargetingValidator : IAmazonTargetingValidator
    {
        private readonly IRepository<AmazonTargeting> _repository;

        public AmazonTargetingValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AmazonTargeting>();
        }

        public ValidationResult Validate(AmazonTargetingModel targeting)
        {
            ValidationResult result = new();

            return result;
        }
    }
}
