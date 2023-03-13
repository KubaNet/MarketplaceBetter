using MarketplaceBetter.Domain.Entities.Sales;
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
    }
}
