using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.Products
{
    public class Variant : Entity
    {
        public string Sku { get; set; }

        public string Asin { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long ColorId { get; set; }

        public virtual Color Color { get; set; }

        public long SizeId { get; set; }

        public virtual Size Size { get; set; }

        public long StatusId { get; set; }

        public virtual EntityStatus Status { get; set; }

        public string Ean { get; set; }

        public override string ToString()
        {
            return Sku;
        }
    }
}
