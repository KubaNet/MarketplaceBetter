using MarketplaceBetter.Domain.Model.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.OperationsModel.Amazon
{
    public class AddKeywordResearchTargetsFromAdvertisingReportModel
    {
        public KeywordResearchModel KeywordResearch { get; set; }

        public IList<AdvertisingReportData> AdvertisingReportData { get; set; } = new List<AdvertisingReportData>();
    }
}
