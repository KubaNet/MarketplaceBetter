using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class AmazonTargetingModel
    {
        public long Id { get; set; }

        public string Value { get; set; }

        public CampaignModel Campaign { get; set; }

        public AmazonTargetingTypeModel Type { get; set; }

        public AmazonTargetingStatusModel Status { get; set; }
    }
}
