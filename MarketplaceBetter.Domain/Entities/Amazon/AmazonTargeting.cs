using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class AmazonTargeting : Entity
    {
        public string Targeting { get; set; }

        public long StatusId { get; set; }

        public virtual AmazonTargetingStatus Status { get; set; }

        public long CampaignId { get; set; }

        public virtual AmazonCampaign Campaign { get; set; }
    }
}
