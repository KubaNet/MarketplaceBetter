using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToTwoDecimalDigits(this double value)
        {
            return value.ToString("N2", CultureInfo.GetCultureInfo("pl-PL"));
        }

        public static string ToTwoDecimalDigits(this double? value)
        {
            if (value.HasValue)
            {
                return ToTwoDecimalDigits(value.Value);
            }

            return null;
        }
    }
}
