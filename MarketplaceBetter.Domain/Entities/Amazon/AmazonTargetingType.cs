using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class AmazonTargetingType : Entity
    {
        public string Name { get; set; }

        public AmazonTargetingTypeEnum SystemName { get; set; }
    }
}
