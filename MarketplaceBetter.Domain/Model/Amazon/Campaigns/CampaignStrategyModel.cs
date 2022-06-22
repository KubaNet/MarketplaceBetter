using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class CampaignStrategyModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public CampaignStrategyEnum SystemName { get; set; }

        public CampaignTypeModel CampaignType { get; set; }
    }
}
