using MarketplaceBetter.Domain.Constants;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Inventory
{
    public class AmazonChildModel
    {
        public long Id { get; set; }

        [Required]
        public AmazonParentModel Parent { get; set; }

        [Display(Name = "Product Variant")]
        [Required]
        public ProductVariantModel ProductVariant { get; set; }

        [Display(Name = "SKU")]
        [Required]
        [StringLength(Standard.AmazonSkuMaxLength, MinimumLength = 2)]
        public string Sku { get; set; }

        [Display(Name = "ASIN")]
        [StringLength(Standard.AmazonAsinLength, MinimumLength = 0)]
        public string Asin { get; set; }
    }
}
