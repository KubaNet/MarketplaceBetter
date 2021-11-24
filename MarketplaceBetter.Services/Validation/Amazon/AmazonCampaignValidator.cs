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
