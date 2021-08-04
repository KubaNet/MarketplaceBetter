using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class AmazonParent : Entity
    {
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public string Sku { get; set; }

        public string Asin { get; set; }
    }
}
