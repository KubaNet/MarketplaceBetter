using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Invoicing
{
    public class InvoiceEntryModel
    {
        public long Id { get; set; }

        public string OrderItemId { get; set; }

        public string ShipmentItemId { get; set; }

        public VariantModel Variant { get; set; }

        public long CurrencyId { get; set; }

        public CurrencyModel Currency { get; set; }

        public int Quantity { get; set; }

        public decimal GrossPrice { get; set; }
    }
}
