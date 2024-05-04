using MarketplaceBetter.Domain.Entities.Catalog.Products;

namespace MarketplaceBetter.Domain.Entities.Advertising.Campaigns
{
    public class Campaign : Entity
    {
        public string Name { get; set; }

        public long PortfolioId { get; set; }

        public virtual Portfolio Portfolio { get; set; }

        public long TypeId { get; set; }

        public virtual CampaignType Type { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
