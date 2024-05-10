namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class AdModel
    {
        public long Id { get; set; }

        public string AmazonId { get; set; }

        public AdGroupModel Group { get; set; }

        public AdEntityStateModel State { get; set; }
    }
}
