using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog
{
    public class ProductVariant : Entity
    {
        public string Sku { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long ColorId { get; set; }

        public virtual Color Color { get; set; }

        public long SizeId { get; set; }

        public virtual Size Size { get; set; }

        public override string ToString()
        {
            return Sku;
        }
    }
}
