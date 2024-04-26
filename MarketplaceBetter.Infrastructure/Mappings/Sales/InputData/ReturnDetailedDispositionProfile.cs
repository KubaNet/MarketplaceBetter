using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.InputData;

namespace MarketplaceBetter.Infrastructure.Mappings.Sales.InputData
{
    public class ReturnDetailedDispositionProfile : Profile
    {
        public ReturnDetailedDispositionProfile()
        {
            CreateMap<ReturnDetailedDisposition, ReturnDetailedDispositionModel>();
        }
    }
}
