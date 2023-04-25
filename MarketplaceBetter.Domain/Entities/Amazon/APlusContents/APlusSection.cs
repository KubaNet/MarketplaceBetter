using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusSection : Entity
    {
        public string Name { get; set; }

        public APlusSectionEnum SystemName { get; set; }

        public virtual IList<APlusElement> Elements { get; set; }
    }
}
