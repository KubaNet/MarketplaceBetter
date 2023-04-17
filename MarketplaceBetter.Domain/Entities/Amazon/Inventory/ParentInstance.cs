using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Inventory
{
    public class ParentInstance : Entity
    {
        public string Sku { get; set; }

        public string Asin { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public long StatusId { get; set; }

        public virtual EntityStatus Status { get; set; }

        public long? TemplateId { get; set; }

        public virtual Template Template { get; set; }

        public override string ToString()
        {
            return Sku;
        }
    }
}
