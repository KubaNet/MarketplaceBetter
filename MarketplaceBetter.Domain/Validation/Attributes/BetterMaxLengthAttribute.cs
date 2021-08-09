using MarketplaceBetter.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Validation.Attributes
{
    public class BetterMaxLengthAttribute : StringLengthAttribute
    {
        public BetterMaxLengthAttribute() : base(Standard.StringMaxLength)
        {
        }
    }
}
