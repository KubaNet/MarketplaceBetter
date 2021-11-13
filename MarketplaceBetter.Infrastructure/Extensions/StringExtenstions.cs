using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Extensions
{
    public static class StringExtenstions
    {
        public static int ParseToIntOrDefault(this string numberString)
        {
            int.TryParse(numberString, out int number);

            return number;
        }

        public static IList<string> SplitForFiltering(this string searchString)
        {
            IList<string> searchStrings = new List<string>();
            foreach (var searchStringElement in searchString.Split('&'))
            {
                if (!string.IsNullOrWhiteSpace(searchStringElement))
                {
                    searchStrings.Add(searchStringElement.Trim());
                }
            }

            return searchStrings;
        }
    }
}
