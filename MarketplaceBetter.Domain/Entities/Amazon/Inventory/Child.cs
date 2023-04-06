using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Inventory
{
    public class Child : Entity
    {
        public string Sku { get; set; }

        public string Asin { get; set; }

        public long ParentId { get; set; }

        public virtual Parent Parent { get; set; }

        public long VariantId { get; set; }

        public virtual Variant Variant { get; set; }

        public long StatusId { get; set; }

        public virtual EntityStatus Status { get; set; }

        public override string ToString()
        {
            return Sku;
        }
    }
}
