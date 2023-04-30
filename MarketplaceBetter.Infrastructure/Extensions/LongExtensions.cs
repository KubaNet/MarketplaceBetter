using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Extensions
{
    public static class LongExtensions
    {
        public static long BytesToKilobytes(this long value)
        {
            return (long)Math.Round((double)value / 1024);
        }
    }
}
