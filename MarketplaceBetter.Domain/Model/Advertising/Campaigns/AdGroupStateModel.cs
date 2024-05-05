using MarketplaceBetter.Domain.Entities.Advertising.Campaigns;

namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class AdGroupStateModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public AdGroupStateEnum SystemName { get; set; }
    }
}
