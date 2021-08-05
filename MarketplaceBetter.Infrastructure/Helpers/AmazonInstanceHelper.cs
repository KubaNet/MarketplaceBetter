using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Helpers
{
    public static class AmazonInstanceHelper
    {
        public static string GetCodeFor(InstanceEnum instance)
        {
            return instance switch
            {
                InstanceEnum.AmazonUK => "uk",
                InstanceEnum.AmazonDE => "de",
                _ => throw new UnrecognizedEnumValue<InstanceEnum>(instance)
            };
        }
    }
}
