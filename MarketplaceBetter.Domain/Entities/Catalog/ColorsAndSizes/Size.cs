using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes
{
    public class Size : Entity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsOneSize { get; set; }

        public long GroupId { get; set; }

        public virtual SizeGroup Group { get; set; }

        public long StandardSizeId { get; set; }

        public virtual StandardSize StandardSize { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
