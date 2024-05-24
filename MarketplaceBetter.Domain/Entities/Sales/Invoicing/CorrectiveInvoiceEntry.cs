namespace MarketplaceBetter.Domain.Entities.Sales.Invoicing
{
    public class CorrectiveInvoiceEntry : Entity
    {
        public long InvoiceEntryId { get; set; }

        public virtual InvoiceEntry InvoiceEntry { get; set; }

        public int Quantity { get; set; }

        public decimal GrossPrice { get; set; }
    }
}
