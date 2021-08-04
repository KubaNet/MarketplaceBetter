using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Domain.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class AmazonParentModel
    {
        public long Id { get; set; }

        public ProductModel Product { get; set; }

        public InstanceModel Instance { get; set; }

        public string Sku { get; set; }

        public string Asin { get; set; }
    }
}
