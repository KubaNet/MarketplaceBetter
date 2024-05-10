namespace MarketplaceBetter.Domain.Entities.Advertising.Campaigns
{
    public class Ad : Entity
    {
        public string AmazonId { get; set; }

        public long GroupId { get; set; }

        public virtual AdGroup Group { get; set; }

        public long StateId { get; set; }

        public virtual AdEntityState State { get; set; }
    }
}
