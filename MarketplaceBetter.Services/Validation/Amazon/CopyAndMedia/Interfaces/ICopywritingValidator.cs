using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.CopyAndMedia.Interfaces
{
    public interface ICopywritingValidator
    {
        ValidationResult Validate(CopywritingModel copywriting);
    }
}
