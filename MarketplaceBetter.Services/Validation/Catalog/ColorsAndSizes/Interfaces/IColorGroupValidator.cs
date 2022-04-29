using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.ColorsAndSizes.Interfaces
{
    public interface IColorGroupValidator
    {
        ValidationResult Validate(ColorGroupModel group);
    }
}
