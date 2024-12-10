using MarketplaceBetter.Domain.Model.Catalog.Products;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes
{
    public class HeroColorModel
    {
        public long Id { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        public ColorModel Color { get; set; }
    }
}
