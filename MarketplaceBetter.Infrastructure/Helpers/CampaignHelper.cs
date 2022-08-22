using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Helpers
{
    public static class CampaignHelper
    {
        public static string GetNameFor(InstanceModel instance, CampaignTypeModel type, CampaignStrategyModel strategy, ProductModel product)
        {
            string name = string.Empty;

            if (instance != null && type != null && strategy != null && product != null)
            {
                name = $"{product.Brand.Code}_{product.Name.Replace(" ", "_")}_{strategy.Name.ToUpper()}_{GetTypeCodeFor(type)}_{InstanceHelper.GetCodeFor(instance.SystemName)}";
            }

            return name;
        }

        private static string GetTypeCodeFor(CampaignTypeModel type)
        {
            switch (type.SystemName)
            {
                case CampaignTypeEnum.SponsoredProducts:
                    return "SP";
                case CampaignTypeEnum.SponsoredBrands:
                    return "SB";
                case CampaignTypeEnum.SponsoredDisplay:
                    return "SD";
                default:
                    throw new UnrecognizedEnumValueException<CampaignTypeEnum>(type.SystemName);
            }
        }
    }
}
