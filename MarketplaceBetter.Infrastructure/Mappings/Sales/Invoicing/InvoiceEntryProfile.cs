using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.Invoicing;
using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Sales.Invoicing
{
    public class InvoiceEntryProfile : Profile
    {
        public InvoiceEntryProfile()
        {
            CreateMap<InvoiceEntry, InvoiceEntryModel>();
        }
    }
}
