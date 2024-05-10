using MarketplaceBetter.Domain.Entities.Advertising.Campaigns;

namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class AdEntityStateModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public AdEntityStateEnum SystemName { get; set; }
    }
}
