using CsvHelper;
using CsvHelper.Configuration;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.OperationsModel.Amazon;
using MarketplaceBetter.Services.Operations.Amazon.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Operations.Amazon
{
    public class KeywordResearchTargetOperationsService : IKeywordResearchTargetOperationsService
    {
        public AddKeywordResearchTargetsFromAdvertisingReportModel CreateTargetsFromAdvertisingReport(KeywordResearchModel research, Stream file)
        {
            AddKeywordResearchTargetsFromAdvertisingReportModel model = new AddKeywordResearchTargetsFromAdvertisingReportModel { KeywordResearch = research };

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
                AdvertisingReportData reportData = new AdvertisingReportData
                {
                    Campaign = csv.GetField("Campaign Name"),
                    SearchTerm = csv.GetField("Customer Search Term")
                };

                model.AdvertisingReportData.Add(reportData);
            }

            return model;
        }
    }
}
