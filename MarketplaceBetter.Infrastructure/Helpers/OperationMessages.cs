using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Helpers
{
    public static class OperationMessages
    {
        public static string SuccessfullyAdded(string entityName)
        {
            return string.Format("{0} was successfully added.", entityName);
        }
    }
}
