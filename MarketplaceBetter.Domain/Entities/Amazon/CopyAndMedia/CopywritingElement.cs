using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia
{
    public class CopywritingElement : Entity
    {
        public string Name { get; set; }

        public CopywritingElementEnum SystemName { get; set; }

        public int Order { get; set; }
    }
}
