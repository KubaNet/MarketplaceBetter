using CloudinaryDotNet.Actions;
using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Helpers
{
    public static class PhotoFromCloudHelper
    {
        public static InstanceModel GetIntance(IList<InstanceModel> instances, string fileName)
        {
            foreach (var instance in instances)
            {
                if (fileName.StartsWith($"{instance.ShortName}_", StringComparison.OrdinalIgnoreCase))
                {
                    return instance;
                }
            }

            return instances.Single(i => i.SystemName == InstanceEnum.All);
        }

        public static PhotoTypeModel GetType(IList<PhotoTypeModel> types, string fileName)
        {
            string amazonUploadCode;

            if (fileName.Contains('.'))
            {
                amazonUploadCode = fileName.Substring(fileName.LastIndexOf(".") + 1, fileName.Length - fileName.LastIndexOf(".") - 1);
            }
            else
            {
                return null;
            }

            foreach (var type in types)
            {
                if (type.AmazonUploadCode.Equals(amazonUploadCode, StringComparison.OrdinalIgnoreCase))
                {
                    return type;
                }
            }

            return null;
        }

        private static string GetBrandName(IList<BrandModel> brands, IList<InstanceModel> instances, string fileName)
        {
            if (StartsWithInstance(instances, fileName))
            {
                fileName = fileName.Substring(0, 3);
            }

            return null;
        }

        private static bool StartsWithInstance(IList<InstanceModel> instances, string fileName)
        {
            return instances.Any(i => fileName.StartsWith($"{i.ShortName}_"));
        }
    }
}
