using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog
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

        public override string ToString()
        {
            return Name;
        }
    }
}
