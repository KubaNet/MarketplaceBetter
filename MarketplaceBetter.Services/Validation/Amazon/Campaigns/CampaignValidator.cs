using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Campaigns.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Campaigns
{
    public class CampaignValidator : ICampaignValidator
    {
        private readonly IRepository<Campaign> _repository;

        public CampaignValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Campaign>();
        }

        public ValidationResult Validate(CampaignModel campaign)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != campaign.Id && c.InstanceId == campaign.Instance.Id && c.ProductId == campaign.Product.Id))
            {
                result.AddError("There already exists a Campaign for this Instance and Product.");
            }

            if (_repository.Any(c => c.Id != campaign.Id && c.Name == campaign.Name))
            {
                result.AddErrorFor<CampaignModel>(c => c.Name, ValidationMessages.NameNotUnique, "Campaign");
            }

            return result;
        }
    }
}
