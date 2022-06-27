using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.CsvRecords
{
    public class SponsoredProductCsvRecord
    {
        public string Product { get; set; }

        public string Entity { get; set; }

        public string Operation { get; set; }

        public string CampaignId { get; set; }

        public string AdGroupId { get; set; }

        public string PortfolioId { get; set; }

        public string AdId { get; set; }

        public string KeywordId { get; set; }

        public string ProductTargetingId { get; set; }

        public string CampaignName { get; set; }

        public string AdGroupName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string TargetingType { get; set; }

        public string State { get; set; }

        public string DailyBudget { get; set; }

        public string SKU { get; set; }

        public string ASIN { get; set; }

        public string AdGroupDefaultBid { get; set; }

        public string Bid { get; set; }

        public string KeywordText { get; set; }

        public string MatchType { get; set; }

        public string BiddingStrategy { get; set; }

        public string Placement { get; set; }

        public string Percentage { get; set; }

        public string ProductTargetingExpression { get; set; }
    }
}
