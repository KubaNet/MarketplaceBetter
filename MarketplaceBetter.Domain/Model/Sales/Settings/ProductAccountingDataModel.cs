using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Settings
{
    public class ProductAccountingDataModel
    {
        public long Id { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Display(Name = "Invoice Name")]
        [Required]
        [BetterLength]
        public string InvoiceName { get; set; }

        [Display(Name = "Commodity Code")]
        [Required]
        [BetterLength]
        public string CommodityCode { get; set; }

        [Required]
        public double Weight { get; set; }

        public IList<ProductionCostModel> ProductionCosts { get; set; }
    }
}
