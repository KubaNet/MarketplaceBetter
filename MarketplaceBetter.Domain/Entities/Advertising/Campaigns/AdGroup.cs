using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Advertising.Campaigns
{
    public class AdGroup : Entity
    {
        public string Name { get; set; }

        public string AmazonId { get; set; }

        public long StateId { get; set; }

        public virtual AdGroupState State { get; set; }
    }
}
