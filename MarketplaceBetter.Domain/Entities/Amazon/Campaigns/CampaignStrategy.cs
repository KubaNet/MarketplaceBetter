using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class CampaignStrategy : Entity
    {
        public string Name { get; set; }

        public CampaignStrategyEnum SystemName { get; set; }

        public long CampaignTypeId { get; set; }

        public virtual CampaignType CampaignType { get; set; }
    }
}