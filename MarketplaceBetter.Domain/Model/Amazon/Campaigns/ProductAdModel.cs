using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Campaigns
{
    public class ProductAdModel
    {
        public long Id { get; set; }

        public VariantModel Variant { get; set; }

        public AdGroupModel AdGroup { get; set; }

        public string AmazonId { get; set; }

        public AdEntityStatusModel Status { get; set; }
    }
}
