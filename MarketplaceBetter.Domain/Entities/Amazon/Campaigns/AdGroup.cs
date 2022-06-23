using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class AdGroup : Entity
    {
        public string Name { get; set; }

        public long CampaignId { get; set; }

        public virtual Campaign Campaign { get; set; }

        public string AmazonId { get; set; }

        public long StatusId { get; set; }

        public virtual AdEntityStatus Status { get; set; }
    }
}
