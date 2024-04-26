using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.InputData;

namespace MarketplaceBetter.Infrastructure.Mappings.Sales.InputData
{
    public class ReturnReasonProfile : Profile
    {
        public ReturnReasonProfile()
        {
            CreateMap<ReturnReason, ReturnReasonModel>();
        }
    }
}
