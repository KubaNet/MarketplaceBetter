using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Inventory
{
    public class Parent : Entity
    {
        public string Sku { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public override string ToString()
        {
            return Sku;
        }
    }
}
