using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Helpers
{
    public static class LinkHelper
    {
        public static string GetUrlForChilds(long productId, long instanceId)
        {
            return $"childs?productId={productId}&instanceId={instanceId}&showAll=true";
        }

        public static string GetUrlForVariantPhotosByProduct(long productId)
        {
            return $"variant-photos?productId={productId}&showAll=true";
        }

        public static string GetUrlForVariantPhotosByVariant(long variantId)
        {
            return $"variant-photos?variantId={variantId}&showAll=true";
        }
    }
}
