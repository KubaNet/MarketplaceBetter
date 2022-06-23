using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class AdEntityStatus : Entity
    {
        public string Name { get; set; }

        public AdEntityStatusEnum SystemName { get; set; }
    }
}
