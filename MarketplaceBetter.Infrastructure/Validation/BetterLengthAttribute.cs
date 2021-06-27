using MarketplaceBetter.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Validation
{
    public class BetterLengthAttribute : StringLengthAttribute
    {
        public BetterLengthAttribute() : base(Standard.StringMaxLength)
        {
            MinimumLength = Standard.StringMinLength;
        }

        public BetterLengthAttribute(int maximumLength) : base(maximumLength)
        {
        }
    }
}
