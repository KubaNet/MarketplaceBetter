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
    }
}
