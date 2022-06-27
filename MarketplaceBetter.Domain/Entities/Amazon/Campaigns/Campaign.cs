using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class Campaign : Entity
    {
        public string Name { get; set; }

        public long InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public long TypeId { get; set; }

        public virtual CampaignType Type { get; set; }

        public long StrategyId { get; set; }

        public virtual CampaignStrategy Strategy { get; set; }

        public long? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public decimal DefaultBid { get; set; }

        public string AmazonId { get; set; }

        public long StatusId { get; set; }

        public virtual AdEntityStatus Status { get; set; }
    }
}
