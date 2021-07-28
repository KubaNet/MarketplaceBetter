using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog
{
    public class Color : Entity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public long GroupId { get; set; }

        public ColorGroup Group { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
