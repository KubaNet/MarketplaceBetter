using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class Portfolio : Entity
    {
        public string Name { get; set; }

        public string AmazonId { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }
    }
}
