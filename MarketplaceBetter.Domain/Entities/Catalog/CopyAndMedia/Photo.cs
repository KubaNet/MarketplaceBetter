using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia
{
    public class Photo : Entity
    {
        public long VariantId { get; set; }

        public virtual Variant Variant { get; set; }

        public bool ForAllInstances { get; set; }

        public long? InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public long TypeId { get; set; }

        public virtual PhotoType Type { get; set; }

        public string CloudId { get; set; }

        public string Url { get; set; }
    }
}
