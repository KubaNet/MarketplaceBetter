using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Helpers
{
    public static class LinkHelper
    {
        public static string GetUrlForVariants(long productId)
        {
            return $"variants?productId={productId}&showAll=true";
        }

        public static string GetUrlForChilds(long productId, long instanceId)
        {
            return $"childs?productId={productId}&instanceId={instanceId}&showAll=true";
        }

        public static string GetUrlForVariantPhotosByProduct(long productId, long instanceId)
        {
            return $"variant-photos?productId={productId}&instanceId={instanceId}&showAll=true";
        }

        public static string GetUrlForVariantPhotosByVariant(long variantId, long instanceId)
        {
            return $"variant-photos?variantId={variantId}&instanceId={instanceId}&showAll=true";
        }

        public static string GetUrlForCopywriting(long productId, long instanceid)
        {
            return $"copywriting?productId={productId}&instanceId={instanceid}&showAll=true";
        }
    }
}
