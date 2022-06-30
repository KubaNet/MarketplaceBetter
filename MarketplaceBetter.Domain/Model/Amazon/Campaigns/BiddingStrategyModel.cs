using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class BiddingStrategyModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public BiddingStrategyEnum SystemName { get; set; }
    }
}
