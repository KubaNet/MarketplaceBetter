using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class AdGroupModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal DefaultBid { get; set; }

        public CampaignModel Campaign { get; set; }

        public long? AmazonId { get; set; }

        public AdEntityStatusModel Status { get; set; }
    }
}
