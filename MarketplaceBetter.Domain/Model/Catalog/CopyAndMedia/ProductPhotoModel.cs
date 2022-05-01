using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia
{
    public class ProductPhotoModel
    {
        public VariantModel Variant { get; set; }

        public bool ForAllInstances { get; set; }

        public InstanceModel Instance { get; set; }

        public ProductPhotoTypeModel Type { get; set; }

        public string Url { get; set; }
    }
}
