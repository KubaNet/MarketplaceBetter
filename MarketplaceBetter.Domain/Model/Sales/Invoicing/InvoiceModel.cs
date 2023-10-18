using MarketplaceBetter.Domain.Model.Sales.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Invoicing
{
    public class InvoiceModel
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public string OrderId { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Buyer { get; set; }

        public string BuyerFirstName { get; set; }

        public string BuyerLastName { get; set; }

        public string BuyerStreet { get; set; }

        public string BuyerCity { get; set; }

        public string BuyerPostalCode { get; set; }

        public string BuyerState { get; set; }

        public string BuyerCountry { get; set; }

        public VatRuleModel VatRule { get; set; }

        public decimal ShippingGrossPrice { get; set; }

        public bool IsIssued { get; set; }

        public string ApiNumber { get; set; }

        public string ApiError { get; set; }

        public IList<InvoiceEntryModel> Entries { get; set; }
    }
}
