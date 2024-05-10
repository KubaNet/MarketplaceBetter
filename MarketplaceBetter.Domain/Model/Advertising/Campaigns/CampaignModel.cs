using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;

namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class CampaignModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string AmazonId { get; set; }

        public InstanceModel Instance { get; set; }

        public PortfolioModel Portfolio { get; set; }

        public CampaignTypeModel Type { get; set; }

        public ProductModel Product { get; set; }

        public AdEntityStateModel State { get; set; }
    }
}
