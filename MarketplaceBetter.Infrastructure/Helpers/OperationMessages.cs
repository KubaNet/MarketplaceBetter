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

        public static string SuccessfullyUpdated(string entityName)
        {
            return string.Format("{0} was successfully updated.", entityName);
        }

        public static string SuccessfullyDeleted(string entityName)
        {
            return string.Format("{0} was successfully deleted.", entityName);
        }
    }
}
