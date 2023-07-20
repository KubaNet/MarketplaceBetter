using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia
{
    public class VariantPhotosModel
    {
        public VariantModel Variant { get; set; }

        public InstanceModel Instance { get; set; }

        public IList<PhotoModel> Photos { get; set; }
    }
}
