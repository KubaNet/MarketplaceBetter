using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public class MatchType : Entity
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public MatchTypeEnum SystemName { get; set; }
    }
}
