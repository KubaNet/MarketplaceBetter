namespace MarketplaceBetter.Domain.Entities.Advertising.Campaigns
{
    public class AdEntityState : Entity
    {
        public string Name { get; set; }

        public AdEntityStateEnum SystemName { get; set; }
    }
}
