using MarketplaceBetter.Domain.Entities.Sales.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.Invoicing
{
    public class Invoice : Entity
    {
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

        public long VatRuleId { get; set; }

        public virtual VatRule VatRule { get; set; }

        public decimal ShippingGrossPrice { get; set; }

        public bool IsIssued { get; set; }

        public DateTime? IssueDate { get; set; }

        public string ApiNumber { get; set; }

        public string ApiError { get; set; }

        public virtual IList<InvoiceEntry> Entries { get; set; }
    }
}
