using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
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
        public PortfolioModel Portfolio { get; set; }

        [Required]
        public CampaignTypeModel Type { get; set; }

        [Required]
        public CampaignStrategyModel Strategy { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        [Display(Name = "Default Bid")]
        public decimal DefaultBid { get; set; }

        [Required]
        [Display(Name = "Daily Budget")]
        public int DailyBudget { get; set; }

        [Required]
        [Display(Name = "Top Of Search Bid Adjustment")]
        public int TopOfSearchBidAdjustment { get; set; }

        [Required]
        [Display(Name = "Product Page Bid Adjustment")]
        public int ProductPageBidAdjustment { get; set; }

        [Required]
        [Display(Name = "Bidding Strategy")]
        public BiddingStrategyModel BiddingStrategy { get; set; }

        public string AmazonId { get; set; }

        public AdEntityStatusModel Status { get; set; }
    }
}
