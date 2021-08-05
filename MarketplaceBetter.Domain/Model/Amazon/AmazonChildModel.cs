using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class AmazonChildModel
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        public AmazonParentModel Parent { get; set; }

        public long ProductVariantId { get; set; }

        public ProductVariantModel ProductVariant { get; set; }

        public string Sku { get; set; }

        public string Asin { get; set; }
    }
}
