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

            if (instance != null)
            {
                name = InstanceHelper.GetCodeFor(instance.SystemName);

                if (type != null)
                {
                    name += $"_{GetTypeCodeFor(type)}";

                    if (product != null)
                    {
                        name += $"_{product.Brand.Code}_{product.Name.Replace(" ", "_")}";

                        if (strategy != null)
                        {
                            name += $"_{strategy.Name.ToUpper()}";
                        }
                    }
                    else if (strategy != null)
                    {
                        name += $"_{strategy.Name.ToUpper()}";
                    }
                }
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
