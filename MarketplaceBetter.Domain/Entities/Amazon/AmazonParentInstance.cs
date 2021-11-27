using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class AmazonParentInstance : Entity
    {
        public long ParentId { get; set; }

        public virtual AmazonParent Parent { get; set; }

        public string Sku { get; set; }

        public string Asin { get; set; }
    }
}
