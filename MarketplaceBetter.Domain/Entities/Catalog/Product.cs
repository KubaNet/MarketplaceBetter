using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public long BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}
