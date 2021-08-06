using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Model.ListRequests.Catalog
{
    public class ProductVariantListRequest : ListRequest
    {
        public long? Id { get; set; }

        public string Sku { get; set; }

        public ProductModel Product { get; set; }

        public ColorModel Color { get; set; }

        public SizeModel Size { get; set; }
    }
}
