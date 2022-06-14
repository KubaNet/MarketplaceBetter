using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Keywords
{
    public class ResearchTargetStatusModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ResearchTargetStatusEnum SystemName { get; set; }
    }
}
