using MarketplaceBetter.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class PortfolioModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string AmazonId { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }
    }
}
