namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class AdGroupModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string AmazonId { get; set; }

        public AdEntityStateModel State { get; set; }

        public CampaignModel Campaign { get; set; }
    }
}
