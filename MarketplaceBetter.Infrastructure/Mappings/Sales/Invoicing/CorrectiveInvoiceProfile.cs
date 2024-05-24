using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.Invoicing;
using MarketplaceBetter.Domain.Model.Sales.Invoicing;

namespace MarketplaceBetter.Infrastructure.Mappings.Sales.Invoicing
{
    public class CorrectiveInvoiceProfile : Profile
    {
        public CorrectiveInvoiceProfile()
        {
            CreateMap<CorrectiveInvoice, CorrectiveInvoiceModel>();
        }
    }
}
