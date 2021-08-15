using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.Interfaces
{
    public interface IKeywordDataValidator
    {
        ValidationResult Validate(KeywordDataModel data);

        ValidationResult ValidateDataFile(FileStream dataFile, KeywordDataSourceEnum source);
    }
}
