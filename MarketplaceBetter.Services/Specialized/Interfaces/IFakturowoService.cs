using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface IFakturowoService
    {
        Task<bool> Issue(InvoiceModel invoice);
    }
}
