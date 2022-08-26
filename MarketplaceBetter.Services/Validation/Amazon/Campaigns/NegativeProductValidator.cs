using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Campaigns.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Campaigns
{
    public class NegativeProductValidator : INegativeProductValidator
    {
        private readonly IRepository<NegativeProduct> _repository;

        public NegativeProductValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<NegativeProduct>();
        }

        public ValidationResult Validate(NegativeProductModel negativeProduct)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != negativeProduct.Id && p.AdGroupId == negativeProduct.AdGroup.Id && p.Asin == negativeProduct.Asin))
            {
                result.AddError("There already exists a Negative Product for this Campaign and Asin.");
            }

            return result;
        }
    }
}
