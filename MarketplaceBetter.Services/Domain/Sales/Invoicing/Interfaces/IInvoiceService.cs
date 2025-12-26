using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Invoicing.Interfaces
{
    public interface IInvoiceService
    {
        int CountForListRequest(ListRequest request);

        IList<InvoiceModel> GetForListRequest(ListRequest request);

        void Create(IList<FulfilledShipmentModel> shipments);

        Task Issue(InvoiceModel invoice);
    }
}
