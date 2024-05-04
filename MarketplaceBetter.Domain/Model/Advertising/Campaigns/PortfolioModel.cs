using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class PortfolioModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Amazon Id")]
        [Required]
        [BetterLength]
        public string AmazonId { get; set; }

        public InstanceModel Instance { get; set; }

        public CampaignTypeModel CampaignType { get; set; }
    }
}
