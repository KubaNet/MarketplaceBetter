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

        public Product Product { get; set; }

        public long ColorId { get; set; }

        public Color Color { get; set; }

        public long SizeId { get; set; }

        public Size Size { get; set; }
    }
}
