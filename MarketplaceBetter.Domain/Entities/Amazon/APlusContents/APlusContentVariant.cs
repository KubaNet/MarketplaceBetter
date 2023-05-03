using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusContentVariant : Entity
    {
        public long VariantId { get; set; }

        public virtual Variant Variant { get; set; }
    }
}
