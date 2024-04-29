using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.Inventory
{
    public class ParentProfile : Profile
    {
        public ParentProfile()
        {
            CreateMap<Parent, ParentModel>();
        }
    }
}
