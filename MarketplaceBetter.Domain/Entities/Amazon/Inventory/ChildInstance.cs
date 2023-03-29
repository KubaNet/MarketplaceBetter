using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Inventory
{
    public class ChildInstance : Entity
    {
        public string Sku { get; set; }

        public long ChildId { get; set; }

        public virtual Child Child { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public override string ToString()
        {
            return Sku;
        }
    }
}
