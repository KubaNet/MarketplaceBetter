using MarketplaceBetter.Domain.CsvRecords;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface IAdBulksheetRecordsCreator
    {
        SponsoredProductCsvRecord CreateSponsoredProductsCampaign(Campaign campaign);

        IList<SponsoredProductCsvRecord> CreateSponsoredProductsBiddingAdjustments(Campaign campaign);

        SponsoredProductCsvRecord CreateSponsoredProductsAdGroup(Campaign campaign, AdGroup adGroup);

        IList<SponsoredProductCsvRecord> CreateSponsoredProductsProductAds(Campaign campaign, AdGroup adGroup, IList<ProductAd> productAds);
    }
}
