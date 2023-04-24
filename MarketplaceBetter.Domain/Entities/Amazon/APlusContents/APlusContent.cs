using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusContent : Entity
    {
        public string Name { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public virtual IList<APlusSectionValue> Sections { get; set; }
    }
}
