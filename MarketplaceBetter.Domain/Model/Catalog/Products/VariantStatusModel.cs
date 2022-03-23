using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.Products
{
    public class VariantStatusModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public VariantStatusEnum SystemName { get; set; }
    }
}
