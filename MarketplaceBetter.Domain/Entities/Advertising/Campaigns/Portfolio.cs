using MarketplaceBetter.Domain.Entities.Base;

namespace MarketplaceBetter.Domain.Entities.Advertising.Campaigns
{
    public class Portfolio : Entity
    {
        public string Name { get; set; }

        public string AmazonId { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }
    }
}
