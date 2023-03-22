using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.Products
{
    public class VariantPhotosModel
    {
        public VariantModel Variant { get; set; }

        public IList<PhotoModel> Photos { get; set; }
    }
}
