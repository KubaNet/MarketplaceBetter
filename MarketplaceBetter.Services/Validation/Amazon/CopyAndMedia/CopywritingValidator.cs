using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Services.Validation.Amazon.CopyAndMedia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.CopyAndMedia
{
    public class CopywritingValidator : ICopywritingValidator
    {
        public ValidationResult Validate(CopywritingModel copywriting)
        {
            ValidationResult result = new();

            return result;
        }
    }
}
