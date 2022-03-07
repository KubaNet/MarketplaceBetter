using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes
{
    public class SizeGroup : Entity
    {
        public string Name { get; set; }

        public long BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
