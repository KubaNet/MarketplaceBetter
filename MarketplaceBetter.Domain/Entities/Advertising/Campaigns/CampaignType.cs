namespace MarketplaceBetter.Domain.Entities.Advertising.Campaigns
{
    public class CampaignType : Entity
    {
        public string Name { get; set; }

        public CampaignTypeEnum SystemName { get; set; }
    }
}
