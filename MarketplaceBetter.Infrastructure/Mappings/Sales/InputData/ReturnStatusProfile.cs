using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.InputData;

namespace MarketplaceBetter.Infrastructure.Mappings.Sales.InputData
{
    public class ReturnStatusProfile : Profile
    {
        public ReturnStatusProfile()
        {
            CreateMap<ReturnStatus, ReturnStatusModel>();
        }
    }
}
