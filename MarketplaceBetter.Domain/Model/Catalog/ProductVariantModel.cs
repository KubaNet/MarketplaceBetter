using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog
{
    public class ProductVariantModel
    {
        public long Id { get; set; }

        [Required]
        [BetterLength]
        public string Sku { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        public ColorModel Color { get; set; }

        [Required]
        public SizeModel Size { get; set; }
    }
}