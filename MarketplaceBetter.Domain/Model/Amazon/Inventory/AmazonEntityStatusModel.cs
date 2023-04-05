using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Inventory
{
    public class AmazonEntityStatusModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public AmazonEntityStatusEnum SystemName { get; set; }
    }
}
