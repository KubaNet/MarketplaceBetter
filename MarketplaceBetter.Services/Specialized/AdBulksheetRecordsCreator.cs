using MarketplaceBetter.Domain.CsvRecords;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Services.Specialized.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class AdBulksheetRecordsCreator : IAdBulksheetRecordsCreator
    {
        private readonly IRepository<Child> _childRepository;

        public AdBulksheetRecordsCreator(
            IUnitOfWork unitOfWork)
        {
            _childRepository = unitOfWork.GetRepository<Child>();
        }

        public SponsoredProductCsvRecord CreateSponsoredProductsCampaign(Campaign campaign)
        {
            SponsoredProductCsvRecord record = new SponsoredProductCsvRecord();

            record.Product = "Sponsored Products";
            record.Entity = "Campaign";
            record.CampaignId = GetCampaignId(campaign);
            record.PortfolioId = campaign.Portfolio.AmazonId;
            record.CampaignName = campaign.Name;
            record.StartDate = GetStartDate();
            record.TargetingType = GetTargetingType(campaign.Strategy);
            record.State = campaign.Status.Name;
            record.DailyBudget = campaign.DailyBudget.ToString();
            record.BiddingStrategy = campaign.BiddingStrategy.Name;

            return record;
        }

        public IList<SponsoredProductCsvRecord> CreateSponsoredProductsBiddingAdjustments(Campaign campaign)
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

        public SponsoredProductCsvRecord CreateSponsoredProductsAdGroup(Campaign campaign, AdGroup adGroup)
        {
            SponsoredProductCsvRecord record = new SponsoredProductCsvRecord();

            record.Product = "Sponsored Products";
            record.Entity = "Ad group";
            record.CampaignId = GetCampaignId(campaign);
            record.AdGroupId = GetAdGroupId(adGroup);
            record.AdGroupName = adGroup.Name;
            record.State = adGroup.Status.Name;
            record.AdGroupDefaultBid = campaign.DefaultBid.ToString();

            return record;
        }

        public IList<SponsoredProductCsvRecord> CreateSponsoredProductsProductAds(Campaign campaign, AdGroup adGroup, IList<ProductAd> productAds)
        {
            IList<SponsoredProductCsvRecord> records = new List<SponsoredProductCsvRecord>();

            foreach (var productAd in productAds)
            {
                SponsoredProductCsvRecord record = new SponsoredProductCsvRecord();

                record.Product = "Sponsored Products";
                record.Entity = "Product ad";
                record.CampaignId = GetCampaignId(campaign);
                record.AdGroupId = GetAdGroupId(adGroup);
                record.State = productAd.Status.Name;
                record.SKU = GetProductSku(campaign, productAd.Variant);

                records.Add(record);
            }

            return records;
        }

        private string GetProductSku(Campaign campaign, Variant variant)
        {
            Child child = _childRepository.SingleOrDefault(c => c.Variant.Id == variant.Id && c.InstanceId == campaign.InstanceId);

            if (child != null)
            {
                return child.Sku;
            }
            else
            {
                return $"Brak Child dla wariantu {variant.Sku} i instancji {campaign.Instance.Name}";
            }
        }

        private string GetTargetingType(CampaignStrategy strategy)
        {
            switch (strategy.SystemName)
            {
                case CampaignStrategyEnum.SP_Auto:
                    return "AUTO";
                default:
                    throw new UnrecognizedEnumValueException<CampaignStrategyEnum>(strategy.SystemName);
            }
        }

        private string GetStartDate()
        {
            return DateTime.Today.ToString("yyyyMMdd");
        }

        private string GetCampaignId(Campaign campaign)
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

        private string GetAdGroupId(AdGroup adGroup)
        {
            if (!string.IsNullOrWhiteSpace(adGroup.AmazonId))
            {
                return adGroup.AmazonId;
            }
            else
            {
                return adGroup.Name;
            }
        }
    }
}
