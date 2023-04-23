using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusSection : Entity
    {
        public long TypeId { get; set; }

        public virtual APlusSectionType Type { get; set; }

        public virtual IList<APlusElement> Elements { get; set; }
    }
}
