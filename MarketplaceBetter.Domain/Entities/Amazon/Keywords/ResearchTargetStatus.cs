using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Keywords
{
    public class ResearchTargetStatus : Entity
    {
        public string Name { get; set; }

        public ResearchTargetStatusEnum SystemName { get; set; }
    }
}
