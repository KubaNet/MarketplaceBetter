using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class NegativeProductModel
    {
        public long Id { get; set; }

        [Required]
        public string Asin { get; set; }

        public AdGroupModel AdGroup { get; set; }

        public string AmazonId { get; set; }

        public AdEntityStatusModel Status { get; set; }
    }
}
