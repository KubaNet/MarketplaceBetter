using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia
{
    public class PhotoUploadVariant : Entity
    {
        public long PhotoUploadId { get; set; }

        public virtual PhotoUpload PhotoUpload { get; set; }

        public long VariantId { get; set; }

        public virtual Variant Variant { get; set; }
    }
}
