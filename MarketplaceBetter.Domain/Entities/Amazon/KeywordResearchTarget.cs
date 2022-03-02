using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class KeywordResearchTarget : Entity
    {
        public string Name { get; set; }

        public long ResearchId { get; set; }

        public virtual KeywordResearch Research { get; set; }
    }
}
