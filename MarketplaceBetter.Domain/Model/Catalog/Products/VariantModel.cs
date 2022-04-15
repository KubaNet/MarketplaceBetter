using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.Products
{
    public class VariantModel
    {
        public long Id { get; set; }

        [Display(Name = "SKU")]
        [Required]
        [BetterLength]
        public string Sku { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        public ColorModel Color { get; set; }

        [Required]
        public SizeModel Size { get; set; }

        [Required]
        public VariantStatusModel Status { get; set; }

        [Display(Name = "EAN")]
        [BetterMaxLength]
        public string Ean { get; set; }
    }
}