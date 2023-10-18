using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Services.Domain.Sales.Invocing.Interfaces;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Invocing
{
    public class InvoiceService : IInvoiceService
    {
        public int CountForListRequest(ListRequest request)
        {
            throw new NotImplementedException();
        }

        public IList<InvoiceModel> GetForListRequest(ListRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
