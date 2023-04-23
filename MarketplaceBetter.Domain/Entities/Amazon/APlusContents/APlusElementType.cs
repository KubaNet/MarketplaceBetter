using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusElementType : Entity
    {
        public string Name { get; set; }

        public APlusElementTypeEnum SystemName { get; set; }
    }
}
