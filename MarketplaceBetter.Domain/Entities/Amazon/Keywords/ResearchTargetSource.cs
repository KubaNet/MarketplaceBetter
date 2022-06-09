using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Keywords
{
    public class ResearchTargetSource : Entity
    {
        public string Name { get; set; }

        public ResearchTargetSourceEnum SystemName { get; set; }
    }
}
