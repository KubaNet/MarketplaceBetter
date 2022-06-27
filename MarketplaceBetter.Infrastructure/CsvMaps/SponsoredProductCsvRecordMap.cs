using CsvHelper.Configuration;
using MarketplaceBetter.Domain.CsvRecords;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.CsvMaps
{
    public class SponsoredProductCsvRecordMap : ClassMap<SponsoredProductCsvRecord>
    {
        public SponsoredProductCsvRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(r => r.CampaignId).Name("Campaign Id");
            Map(r => r.AdGroupId).Name("Ad Group Id");
            Map(r => r.AdGroupId).Name("Portfolio Id");
            Map(r => r.AdId).Name("Ad Id (Read only)");
            Map(r => r.KeywordId).Name("Keyword Id (Read only)");
            Map(r => r.ProductTargetingId).Name("Product Targeting Id (Read only)");
            Map(r => r.CampaignId).Name("Campaign Name");
            Map(r => r.AdGroupName).Name("Ad Group Name");
            Map(r => r.StartDate).Name("Start Date");
            Map(r => r.EndDate).Name("End Date");
            Map(r => r.TargetingType).Name("Targeting Type");
            Map(r => r.DailyBudget).Name("Daily Budget");
            Map(r => r.AdGroupDefaultBid).Name("Ad Group Default Bid");
            Map(r => r.KeywordText).Name("KeywordText");
            Map(r => r.MatchType).Name("Match Type");
            Map(r => r.BiddingStrategy).Name("Bidding Strategy");
            Map(r => r.ProductTargetingExpression).Name("Product Targeting Expression");
        }
    }
}
