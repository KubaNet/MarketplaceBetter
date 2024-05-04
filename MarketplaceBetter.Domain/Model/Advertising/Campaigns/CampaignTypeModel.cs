using MarketplaceBetter.Domain.Entities.Advertising.Campaigns;

namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class CampaignTypeModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public CampaignTypeEnum SystemName { get; set; }
    }
}
