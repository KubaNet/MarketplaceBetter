using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class CampaignType : Entity
    {
        public string Name { get; set; }

        public CampaignTypeEnum SystemName { get; set; }
    }
}
