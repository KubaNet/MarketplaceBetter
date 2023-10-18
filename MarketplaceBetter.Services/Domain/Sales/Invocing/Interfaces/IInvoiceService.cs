using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Invocing.Interfaces
{
    public interface IInvoiceService
    {
        int CountForListRequest(ListRequest request);

        IList<InvoiceModel> GetForListRequest(ListRequest request);
    }
}
