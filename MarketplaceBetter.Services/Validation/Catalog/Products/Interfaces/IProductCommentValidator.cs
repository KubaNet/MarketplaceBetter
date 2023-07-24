using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.Products.Interfaces
{
    public interface IProductCommentValidator
    {
        ValidationResult Validate(string comment);
    }
}
