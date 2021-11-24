using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class AmazonCampaignModel
    {
        public long Id { get; set; }

        [Required]
        [BetterLength]
        public string Name { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }
    }
}
