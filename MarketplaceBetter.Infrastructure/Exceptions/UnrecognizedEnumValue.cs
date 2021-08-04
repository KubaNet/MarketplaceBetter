using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Exceptions
{
    public class UnrecognizedEnumValue<T> : Exception where T: Enum
    {
        public UnrecognizedEnumValue(Enum value) : base(GetMessage(value))
        {
        }

        private static string GetMessage(Enum value)
        {
            return string.Format("Unrecognized value of {0}. Value: {1}", value.GetType().Name, value.ToString());
        }
    }
}
