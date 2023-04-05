using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Inventory
{
    public enum AmazonEntityStatusEnum
    {
        None = 0,

        Draft = 1,

        ToAdd = 2,

        Active = 3,

        ToWithdrawn = 4,

        Withdrawn = 5
    }
}
