namespace MarketplaceBetter.Domain.Model.Sales.Invoicing
{
    public class CorrectiveInvoiceModel
    {
        public long Id { get; set; }

        public InvoiceModel Invoice { get; set; }

        public string Number { get; set; }

        public bool IsIssued { get; set; }

        public string ApiNumber { get; set; }

        public string ApiError { get; set; }
    }
}
