using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes
{
    public class StandardSize : Entity
    {
        public string Name { get; set; }

        public StandardSizeEnum SystemName { get; set; }
    }
}
