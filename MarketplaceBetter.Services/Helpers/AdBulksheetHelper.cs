using MarketplaceBetter.Domain.CsvRecords;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Helpers
{
    public static class AdBulksheetHelper
    {
        public static SponsoredProductCsvRecord CreateSponsoredProductsCampaign(Campaign campaign)
        {
            SponsoredProductCsvRecord record = new SponsoredProductCsvRecord();

            record.Product = "Sponsored Products";
            record.Entity = "Campaign";
            record.CampaignId = GetCampaignId(campaign);
            record.PortfolioId = campaign.Portfolio.AmazonId;
            record.CampaignName = campaign.Name;
            record.StartDate = GetStartDate();
            record.TargetingType = GetTargetingType(campaign.Strategy);
            record.State = campaign.Status.SystemName.ToString();
            record.DailyBudget = campaign.DailyBudget.ToString();
            record.BiddingStrategy = campaign.BiddingStrategy.Name;

            return record;
        }

        public static IList<SponsoredProductCsvRecord> CreateSponsoredProductsBiddingAdjustments(Campaign campaign)
        {
            IList<SponsoredProductCsvRecord> records = new List<SponsoredProductCsvRecord>();

            SponsoredProductCsvRecord topOfSearchRecord = new SponsoredProductCsvRecord { Placement = "placementTop", Percentage = campaign.TopOfSearchBidAdjustment.ToString() };
            records.Add(topOfSearchRecord);

            SponsoredProductCsvRecord productPageRecord = new SponsoredProductCsvRecord { Placement = "placementProductPage", Percentage = campaign.ProductPageBidAdjustment.ToString() };
            records.Add(productPageRecord);

            foreach (var record in records)
            {
                record.Product = "Sponsored Products";
                record.Entity = "Bidding adjustment";
                record.CampaignId = GetCampaignId(campaign);
                record.BiddingStrategy = campaign.BiddingStrategy.Name;
            }

            return records;
        }

        private static string GetTargetingType(CampaignStrategy strategy)
        {
            switch (strategy.SystemName)
            {
                case CampaignStrategyEnum.SP_Auto:
                    return "AUTO";
                default:
                    throw new UnrecognizedEnumValueException<CampaignStrategyEnum>(strategy.SystemName);
            }
        }

        private static string GetStartDate()
        {
            return DateTime.Today.ToString("yyyyMMdd");
        }

        private static string GetCampaignId(Campaign campaign)
        {
            if (!string.IsNullOrWhiteSpace(campaign.AmazonId))
            {
                return campaign.AmazonId;
            }
            else
            {
                return campaign.Name;
            }
        }
    }
}
