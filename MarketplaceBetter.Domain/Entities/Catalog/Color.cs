using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog
{
    public class ColorGroup : Entity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
