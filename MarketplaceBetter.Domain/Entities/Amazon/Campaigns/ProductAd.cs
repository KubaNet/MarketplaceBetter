using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class ProductAd : Entity
    {
        public long VariantId { get; set; }

        public virtual Variant Variant { get; set; }

        public long AdGroupId { get; set; }

        public virtual AdGroup AdGroup { get; set; }

        public long? AmazonId { get; set; }

        public long StatusId { get; set; }

        public virtual AdEntityStatus Status { get; set; }
    }
}
