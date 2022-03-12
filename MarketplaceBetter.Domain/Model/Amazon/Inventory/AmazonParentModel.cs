using MarketplaceBetter.Domain.Constants;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Inventory
{
    public class AmazonParentModel
    {
        public long Id { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Display(Name = "SKU")]
        [Required]
        [StringLength(Standard.AmazonSkuMaxLength, MinimumLength = 2)]
        public string Sku { get; set; }

        [Display(Name = "Child SKU")]
        [Required]
        [BetterLength]
        public string ChildSku { get; set; }
    }
}
