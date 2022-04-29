using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia
{
    public class CopywritingModel
    {
        public long Id { get; set; }

        public ProductModel Product { get; set; }

        public InstanceModel Instance { get; set; }

        public CopywritingElementModel Element { get; set; }

        public string Value { get; set; }
    }
}
