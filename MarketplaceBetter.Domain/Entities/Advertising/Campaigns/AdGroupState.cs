namespace MarketplaceBetter.Domain.Entities.Advertising.Campaigns
{
    public class AdGroupState : Entity
    {
        public string Name { get; set; }

        public AdGroupStateEnum SystemName { get; set; }
    }
}
