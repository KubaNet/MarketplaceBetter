using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;

namespace MarketplaceBetter.Infrastructure.Mappings.Catalog.ColorsAndSizes
{
    public class HeroColorProfile : Profile
    {
        public HeroColorProfile()
        {
            CreateMap<HeroColor, HeroColorModel>();
        }
    }
}
