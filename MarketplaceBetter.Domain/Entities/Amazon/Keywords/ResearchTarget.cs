using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Keywords
{
    public class ResearchTarget : Entity
    {
        public string Name { get; set; }

        public long ResearchId { get; set; }

        public virtual Research Research { get; set; }

        public int Helium10Value { get; set; }

        public int AmazonSearchTermsValue { get; set; }

        public long StatusId { get; set; }

        public virtual ResearchTargetStatus Status { get; set; }
    }
}
