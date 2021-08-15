using CsvHelper;
using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Services.Validation.Amazon.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon
{
    public class KeywordDataValidator : IKeywordDataValidator
    {
        private readonly IRepository<KeywordData> _repository;

        public KeywordDataValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<KeywordData>();
        }

        public ValidationResult Validate(KeywordDataModel data)
        {
            ValidationResult result = new();

            return result;
        }

        public ValidationResult ValidateDataFile(FileStream dataFile, KeywordDataSourceEnum source)
        {
            return source switch
            {
                KeywordDataSourceEnum.AmazonSearchTerms => ValidateAmazonSearchTermsFile(dataFile),
                _ => throw new UnrecognizedEnumValue<KeywordDataSourceEnum>(source)
            };
        }

        private ValidationResult ValidateAmazonSearchTermsFile(FileStream dataFile)
        {
            ValidationResult result = new();

            using var streamReader = new StreamReader(dataFile);
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            csv.Read();
            csv.Read();
            csv.ReadHeader();

            if (!csv.HeaderRecord.Any(h => h == "Search Term"))
            {
                result.AddError("There is no Search Term column.");
            }

            if (!csv.HeaderRecord.Any(h => h == "Search Frequency Rank"))
            {
                result.AddError("There is no Search Frequency Rank column.");
            }

            if (!result.IsValid)
            {
                return result;
            }

            while (csv.Read())
            {
                string phrase = csv.GetField("Search Term");
                if (string.IsNullOrWhiteSpace(phrase))
                {
                    result.AddError("Row nr {0} is invalid. There is no Phrase. (row value: {1}).", csv.Parser.Row, csv.Parser.RawRecord);
                }

                string stringValue = csv.GetField("Search Frequency Rank").Replace(",", string.Empty);
                int intValue;
                if (!int.TryParse(stringValue, out intValue))
                {
                    result.AddError("Row nr {0} is invalid. Can't parse value {1} to integer number.", csv.Parser.Row, stringValue);
                }
            }

            return result;
        }
    }
}
