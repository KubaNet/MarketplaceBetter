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
                InstanceEnum.AmazonUS => "us",
                InstanceEnum.AmazonCA => "ca",
                InstanceEnum.AmazonUK => "uk",
                InstanceEnum.AmazonDE => "de",
                InstanceEnum.AmazonFR => "fr",
                InstanceEnum.AmazonIT => "it",
                InstanceEnum.AmazonES => "es",
                InstanceEnum.AmazonNL => "nl",
                InstanceEnum.AmazonSE => "se",
                _ => throw new UnrecognizedEnumValueException<InstanceEnum>(instance)
            };
        }
    }
}
