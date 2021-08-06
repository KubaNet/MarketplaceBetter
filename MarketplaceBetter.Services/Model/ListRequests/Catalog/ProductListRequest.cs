using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Model.ListRequests.Catalog
{
    public class ProductListRequest : ListRequest
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public BrandModel Brand { get; set; }

        public ColorGroupModel ColorGroup { get; set; }

        public SizeGroupModel SizeGroup { get; set; }
    }
}
