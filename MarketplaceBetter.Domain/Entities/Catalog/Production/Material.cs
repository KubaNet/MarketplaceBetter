using MarketplaceBetter.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.Production
{
    public class Material : Entity
    {
        public string Name { get; set; }

        public string SupplierCode { get; set; }

        public long KindId { get; set; }

        public virtual MaterialKind Kind { get; set; }

        public long SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public decimal PricePLN { get; set; }

        public long UnitId { get; set; }

        public virtual MeasurementUnit Unit { get; set; }
    }
}
