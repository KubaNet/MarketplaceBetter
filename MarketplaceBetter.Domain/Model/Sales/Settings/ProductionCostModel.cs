using MarketplaceBetter.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Settings
{
    public class ProductionCostModel
    {
        public long Id { get; set; }

        public decimal Cost { get; set; }

        public CurrencyModel Currency { get; set; }
    }
}
