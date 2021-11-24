using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class AmazonTargetingStatus : Entity
    {
        public string Name { get; set; }

        public AmazonTargetingStatusEnum SystemName { get; set; }
    }
}
