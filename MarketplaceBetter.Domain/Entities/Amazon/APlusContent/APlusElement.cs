using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContent
{
    public class APlusElement : Entity
    {
        public string Name { get; set; }

        public long TypeId { get; set; }
        
        public virtual APlusElementType Type { get; set; }
    }
}
