using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Inventory
{
    public class AmazonEntityStatus : Entity
    {
        public string Name { get; set; }

        public AmazonEntityStatusEnum SystemName { get; set; }
    }
}
