using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Catalog
{
    public class AmazonParentInstance : Entity
    {
        public string Sku { get; set; }

        public string Asin { get; set; }

        public long ParentId { get; set; }

        public virtual AmazonParent Parent { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public override string ToString()
        {
            return Sku;
        }
    }
}
