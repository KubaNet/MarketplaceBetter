using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class BiddingStrategy : Entity
    {
        public string Name { get; set; }

        public BiddingStrategyEnum SystemName { get; set; }
    }
}
