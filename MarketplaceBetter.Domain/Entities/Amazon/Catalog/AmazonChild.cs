using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Catalog
{
    public class AmazonChild : Entity
    {
        public string Sku { get; set; }

        public string Asin { get; set; }

        public long ParentId { get; set; }

        public virtual AmazonParent Parent { get; set; }

        public long ProductVariantId { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }

        public override string ToString()
        {
            return Sku;
        }
    }
}
