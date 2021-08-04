using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales
{
    public class Instance : Entity
    {
        public string Name { get; set; }

        public InstanceEnum SystemName { get; set; }

        public long SalesChannelId { get; set; }

        public virtual SalesChannel SalesChannel { get; set; }
    }
}
