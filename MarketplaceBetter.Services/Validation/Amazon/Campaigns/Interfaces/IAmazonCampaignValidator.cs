using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.Campaigns.Interfaces
{
    public interface IAmazonCampaignValidator
    {
        ValidationResult Validate(AmazonCampaignModel campaign);
    }
}
