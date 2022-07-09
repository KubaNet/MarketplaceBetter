using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class NegativeKeywordTargetingModel
    {
        public long Id { get; set; }

        public string Keyword { get; set; }

        public MatchTypeModel MatchType { get; set; }

        public AdGroupModel AdGroup { get; set; }

        public string AmazonId { get; set; }

        public AdEntityStatusModel Status { get; set; }
    }
}
