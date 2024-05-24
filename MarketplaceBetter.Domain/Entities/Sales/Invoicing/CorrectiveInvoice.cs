namespace MarketplaceBetter.Domain.Entities.Sales.Invoicing
{
    public class CorrectiveInvoice : Entity
    {
        public long InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public string Number { get; set; }

        public bool IsIssued { get; set; }

        public string ApiNumber { get; set; }

        public string ApiError { get; set; }
    }
}
