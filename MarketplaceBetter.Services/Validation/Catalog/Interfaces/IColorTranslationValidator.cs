using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.Interfaces
{
    public interface IColorTranslationValidator
    {
        ValidationResult Validate(ColorTranslationModel translation);
    }
}
