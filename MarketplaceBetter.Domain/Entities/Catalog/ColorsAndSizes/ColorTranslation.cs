using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes
{
    public class ColorTranslation : Entity
    {
        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public long ColorId { get; set; }

        public virtual Color Color { get; set; }

        public string Translation { get; set; }

        public string Mapping { get; set; }
    }
}
