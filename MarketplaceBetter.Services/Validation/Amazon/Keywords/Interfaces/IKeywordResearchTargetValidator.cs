using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.Keywords.Interfaces
{
    public interface IKeywordResearchTargetValidator
    {
        ValidationResult Validate(KeywordTargetModel target);
    }
}
