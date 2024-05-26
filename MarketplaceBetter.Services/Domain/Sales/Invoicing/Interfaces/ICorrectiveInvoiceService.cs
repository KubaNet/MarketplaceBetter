using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Invoicing.Interfaces
{
    public interface ICorrectiveInvoiceService
    {
        int CountForListRequest(ListRequest request);

        IList<CorrectiveInvoiceModel> GetForListRequest(ListRequest request);

        void Create(IList<CustomerReturnModel> returns);

        Task<int> Issue(CorrectiveInvoiceModel correctiveInvoice, int nextNumber);
    }
}
