using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.Products
{
    public class ProductColorModel
    {
        public ProductModel Product { get; set; }

        public ColorModel Color { get; set; }

        public VariantStatusModel Status { get; set; }

        public IList<SizeModel> Sizes { get; set; }
    }
}
