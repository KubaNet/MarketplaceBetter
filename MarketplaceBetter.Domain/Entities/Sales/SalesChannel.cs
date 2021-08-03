using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales
{
    public class SalesChannel : Entity
    {
        public string Name { get; set; }

        public SalesChannelEnum SystemName { get; set; }
    }
}
