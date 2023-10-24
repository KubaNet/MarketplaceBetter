using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.Invoicing
{
    public class InvoiceEntry : Entity
    {
        public string OrderItemId { get; set; }

        public string ShipmentItemId { get; set; }

        public string InvoiceName { get; set; }

        public long VariantId { get; set; }

        public virtual Variant Variant { get; set; }

        public long CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public int Quantity { get; set; }

        public decimal GrossPrice { get; set; }
    }
}
