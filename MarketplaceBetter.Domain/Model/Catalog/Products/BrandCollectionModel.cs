using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.Products
{
    public class BrandCollectionModel
    {
        public string Name { get; set; }

        public BrandModel Brand { get; set; }

        public bool IsBrand { get; set; }

        public CollectionModel Collection { get; set; }

        public bool IsCollection { get; set; }
    }
}
