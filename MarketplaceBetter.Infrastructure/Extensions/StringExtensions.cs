using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static int ParseToIntOrDefault(this string numberString)
        {
            int.TryParse(numberString, out int number);

            return number;
        }

        public static double ParseToDoubleOrDefault(this string numberString)
        {
            double.TryParse(numberString, out double number);

            return number;
        }

		public static decimal ParseToDecimalOrDefault(this string numberString)
		{
			decimal.TryParse(numberString, out decimal number);

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

        public static string NormalizeFromPolish(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            char[] charArray = text.ToCharArray();
            char[] normalizedArray = new char[charArray.Length];

            for (int i = 0; i < normalizedArray.Length; i++)
            {
                normalizedArray[i] = NormalizeChar(charArray[i]);
            }

            return new string(normalizedArray);
        }

        public static string WithoutSpaces(this string text)
        {
            return text.Replace(' ', '_');
        }

        private static char NormalizeChar(char c)
        {
            switch (c)
            {
                case 'ą':
                    return 'a';
                case 'ć':
                    return 'c';
                case 'ę':
                    return 'e';
                case 'ł':
                    return 'l';
                case 'ń':
                    return 'n';
                case 'ó':
                    return 'o';
                case 'ś':
                    return 's';
                case 'ż':
                case 'ź':
                    return 'z';
                default:
                    return c;
            }
        }
    }
}
