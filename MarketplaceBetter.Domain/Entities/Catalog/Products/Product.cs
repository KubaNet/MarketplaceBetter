using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public long BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public long CollectionId { get; set; }

        public virtual Collection Collection { get; set; }

        public long ColorGroupId { get; set; }

        public virtual ColorGroup ColorGroup { get; set; }

        public long SizeGroupId { get; set; }

        public virtual SizeGroup SizeGroup { get; set; }

        public int Order { get; set; }

        public long StatusId { get; set; }

        public virtual EntityStatus Status { get; set; }

        public string Comment { get; set; }

        public bool IsSizeCopy { get; set; }

        public virtual IList<Variant> Variants { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
