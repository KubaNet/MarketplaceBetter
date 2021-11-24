using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class AmazonTargetingModel
    {
        public long Id { get; set; }

        public string Value { get; set; }

        public AmazonCampaignModel Campaign { get; set; }

        public AmazonTargetingTypeModel Type { get; set; }

        public AmazonTargetingStatusModel Status { get; set; }
    }
}
