using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class AmazonTargeting : Entity
    {
        public string Value { get; set; }

        public long CampaignId { get; set; }

        public virtual AmazonCampaign Campaign { get; set; }

        public long TypeId { get; set; }

        public virtual AmazonTargetingType Type { get; set; }

        public long StatusId { get; set; }

        public virtual AmazonTargetingStatus Status { get; set; }
    }
}
