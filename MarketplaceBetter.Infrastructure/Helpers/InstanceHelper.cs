using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Helpers
{
    public static class InstanceHelper
    {
        public static string GetCodeFor(InstanceEnum instance)
        {
            return instance switch
            {
                InstanceEnum.US => "us",
                InstanceEnum.CA => "ca",
                InstanceEnum.UK => "uk",
                InstanceEnum.DE => "de",
                InstanceEnum.FR => "fr",
                InstanceEnum.IT => "it",
                InstanceEnum.ES => "es",
                InstanceEnum.NL => "nl",
                InstanceEnum.SE => "se",
                _ => throw new UnrecognizedEnumValueException<InstanceEnum>(instance)
            };
        }

        public static string GetUrlFor(InstanceEnum instance)
        {
            return instance switch
            {
                InstanceEnum.US => "https://www.amazon.com/",
                InstanceEnum.CA => "https://www.amazon.ca/",
                InstanceEnum.UK => "https://www.amazon.co.uk/",
                InstanceEnum.DE => "https://www.amazon.de/",
                InstanceEnum.FR => "https://www.amazon.fr/",
                InstanceEnum.IT => "https://www.amazon.it/",
                InstanceEnum.ES => "https://www.amazon.es/",
                InstanceEnum.NL => "https://www.amazon.nl/",
                InstanceEnum.SE => "https://www.amazon.se/",
                _ => throw new UnrecognizedEnumValueException<InstanceEnum>(instance)
            };
        }

        public static string GetUrlForProductPage(InstanceEnum instance, string asin)
        {
            return $"{GetUrlFor(instance)}dp\\{asin}";
        }
    }
}
