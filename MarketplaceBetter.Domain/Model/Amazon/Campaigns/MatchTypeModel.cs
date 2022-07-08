using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class MatchTypeModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public MatchTypeEnum SystemName { get; set; }
    }
}
