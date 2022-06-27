using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class CampaignModel
    {
        public long Id { get; set; }

        [Required]
        [BetterLength]
        public string Name { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }

        [Required]
        public CampaignTypeModel Type { get; set; }

        [Required]
        public CampaignStrategyModel Strategy { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        public decimal DefaultBid { get; set; }

        public string AmazonId { get; set; }

        public AdEntityStatusModel Status { get; set; }
    }
}
