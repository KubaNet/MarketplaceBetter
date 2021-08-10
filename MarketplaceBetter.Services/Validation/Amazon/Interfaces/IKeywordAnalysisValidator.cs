using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.Interfaces
{
    public interface IKeywordAnalysisValidator
    {
        ValidationResult Validate(KeywordAnalysisModel analysis);
    }
}
