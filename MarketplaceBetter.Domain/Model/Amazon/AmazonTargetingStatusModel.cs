using MarketplaceBetter.Domain.Entities.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class AmazonTargetingStatusModel
    {
        public string Name { get; set; }

        public AmazonTargetingStatusEnum SystemName { get; set; }
    }
}
