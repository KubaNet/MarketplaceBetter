using MarketplaceBetter.Domain.Model.Catalog.Attributes;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.Attributes.Interfaces
{
    public interface IProductDimensionsValidator
    {
        ValidationResult Validate(ProductDimensionsModel dimensions);
    }
}
