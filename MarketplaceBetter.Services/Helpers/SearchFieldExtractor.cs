using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Helpers
{
    public static class SearchFieldExtractor
    {
        public static SearchField ExtractFrom(string searchString, string[] fieldNames)
        {
            if (!searchString.Contains(':'))
            {
                return null;
            }

            string fieldName = searchString.Trim().Replace(" ", "_");
            fieldName = fieldName[..searchString.IndexOf(':')].ToLower();

            string originalFieldName = fieldNames.SingleOrDefault(n => n == fieldName);

            if (originalFieldName == null)
            {
                return null;
            }

            string value = searchString[(searchString.IndexOf(':') + 1)..].Trim();

            return new SearchField { Name = originalFieldName, Value = value };
        }
    }
}
