using MarketplaceBetter.Domain.Model.Catalog.Products;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class CampaignModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [Required]
        public PortfolioModel Portfolio { get; set; }

        public CampaignTypeModel Type { get; set; }

        [Required]
        public ProductModel Product { get; set; }
    }
}
