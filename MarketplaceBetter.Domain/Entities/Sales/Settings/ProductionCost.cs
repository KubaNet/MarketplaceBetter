using MarketplaceBetter.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.Settings
{
    public class ProductionCost : Entity
    {
        public decimal Cost { get; set; }

        public long CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
