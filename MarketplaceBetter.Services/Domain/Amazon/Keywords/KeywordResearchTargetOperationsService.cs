using CsvHelper;
using CsvHelper.Configuration;
using MarketplaceBetter.Domain.Model.Amazon.Catalog;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords
{
    public class KeywordResearchTargetOperationsService : IKeywordResearchTargetOperationsService
    {
        public AddAdvertisingReportKeywordTargetsModel CreateTargetsFromAdvertisingReport(KeywordResearchModel research, Stream file)
        {
            AddAdvertisingReportKeywordTargetsModel model = new AddAdvertisingReportKeywordTargetsModel { KeywordResearch = research };

            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
            };

            using var reader = new StreamReader(file);
            using var csv = new CsvReader(reader, config);

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                AdvertisingReportKeywordTargetModel reportData = new AdvertisingReportKeywordTargetModel
                {
                    Campaign = csv.GetField("Campaign Name"),
                    SearchTerm = csv.GetField("Customer Search Term")
                };

                model.Targets.Add(reportData);
            }

            return model;
        }
    }
}
