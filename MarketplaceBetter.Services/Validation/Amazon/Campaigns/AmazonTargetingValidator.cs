using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Campaigns.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Campaigns
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
