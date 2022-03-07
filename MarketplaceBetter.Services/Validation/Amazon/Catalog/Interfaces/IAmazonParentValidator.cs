using MarketplaceBetter.Domain.Model.Amazon.Catalog;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.Catalog.Interfaces
{
    public interface IAmazonParentValidator
    {
        ValidationResult Validate(AmazonParentModel parent);
    }
}
