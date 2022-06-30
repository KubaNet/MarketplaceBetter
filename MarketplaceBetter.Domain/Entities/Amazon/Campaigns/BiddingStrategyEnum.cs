using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public enum BiddingStrategyEnum
    {
        None = 0,

        DynamiBidsDownOnly = 1,

        DynamiBidsUpAndDown = 2,

        FixedBids = 3
    }
}
