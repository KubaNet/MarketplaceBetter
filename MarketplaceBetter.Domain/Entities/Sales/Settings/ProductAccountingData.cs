using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.Settings
{
    public class ProductAccountingData : Entity
    {
        public ProductAccountingData()
        {
            ProductionCosts = new List<ProductionCost>();
        }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string InvoiceName { get; set; }

        public string CommodityCode { get; set; }

        public double Weight { get; set; }

        public virtual IList<ProductionCost> ProductionCosts { get; set; }
    }
}
