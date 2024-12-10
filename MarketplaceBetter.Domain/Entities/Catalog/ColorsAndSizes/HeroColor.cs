using MarketplaceBetter.Domain.Entities.Catalog.Products;

namespace MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes
{
    public class HeroColor : Entity
    {
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long ColorId { get; set; }

        public virtual Color Color { get; set; }
    }
}
