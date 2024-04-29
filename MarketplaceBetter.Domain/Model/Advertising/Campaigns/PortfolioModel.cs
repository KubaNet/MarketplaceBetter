using MarketplaceBetter.Domain.Model.Base;

namespace MarketplaceBetter.Domain.Model.Advertising.Campaigns
{
    public class PortfolioModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string AmazonId { get; set; }

        public InstanceModel Instance { get; set; }
    }
}
