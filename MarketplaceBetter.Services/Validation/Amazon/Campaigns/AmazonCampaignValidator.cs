using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Campaigns.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Campaigns
{
    public class AmazonCampaignValidator : IAmazonCampaignValidator
    {
        private readonly IRepository<AmazonCampaign> _repository;

        public AmazonCampaignValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AmazonCampaign>();
        }

        public ValidationResult Validate(AmazonCampaignModel campaign)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != campaign.Id && p.InstanceId == campaign.Instance.Id && p.ProductId == campaign.Product.Id))
            {
                result.AddError("There already exists an Amazon Campaign for this Instance and Product.");
            }

            if (_repository.Any(p => p.Id != campaign.Id && p.Name == campaign.Name))
            {
                result.AddErrorFor<AmazonCampaignModel>(p => p.Name, ValidationMessages.NameNotUnique, "Amazon Campaign");
            }

            return result;
        }
    }
}
