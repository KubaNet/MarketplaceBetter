using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Settings
{
    public class ProductAccountingDataModel
    {
        public long Id { get; set; }

        public ProductModel Product { get; set; }

        public string InvoiceName { get; set; }

        public string CommodityCode { get; set; }

        public double Weight { get; set; }

        public IList<ProductionCostModel> ProductionCosts { get; set; }
    }
}
