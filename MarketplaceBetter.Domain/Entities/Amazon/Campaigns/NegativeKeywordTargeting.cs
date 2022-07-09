using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class NegativeKeywordTargeting : Entity
    {
        public string Keyword { get; set; }

        public long MatchTypeId { get; set; }

        public virtual MatchType MatchType { get; set; }

        public long AdGroupId { get; set; }

        public virtual AdGroup AdGroup { get; set; }

        public string AmazonId { get; set; }

        public long StatusId { get; set; }

        public virtual AdEntityStatus Status { get; set; }
    }
}
