using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusContent : Entity
    {
        public APlusContent()
        {
            Sections = new List<APlusSectionValue>();
            Variants = new List<APlusContentVariant>();
        }

        public string Name { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public long StatusId { get; set; }

        public virtual EntityStatus Status { get; set; }

        public virtual IList<APlusSectionValue> Sections { get; set; }

        public virtual IList<APlusContentVariant> Variants { get; set; }
    }
}
