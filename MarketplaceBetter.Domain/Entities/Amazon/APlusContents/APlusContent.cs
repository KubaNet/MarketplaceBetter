using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusContent : Entity
    {
        public string Name { get; set; }

        public virtual IList<APlusSectionValue> Sections { get; set; }
    }
}
