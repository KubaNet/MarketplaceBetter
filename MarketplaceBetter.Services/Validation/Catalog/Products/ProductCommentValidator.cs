using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Services.Validation.Catalog.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.Products
{
    public class ProductCommentValidator : IProductCommentValidator
    {
        public ValidationResult Validate(string comment)
        {
            ValidationResult result = new ValidationResult();

            return result;
        }
    }
}
