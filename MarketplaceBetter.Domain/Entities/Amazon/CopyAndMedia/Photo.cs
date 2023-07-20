using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia
{
    public class Photo : Entity
    {
        public long VariantId { get; set; }

        public virtual Variant Variant { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public long TypeId { get; set; }

        public virtual PhotoType Type { get; set; }

        public long KindId { get; set; }

        public virtual PhotoKind Kind { get; set; }

        public string CloudId { get; set; }

        public string Version { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public DateTime Uploaded { get; set; }
    }
}
