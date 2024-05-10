using AutoMapper;
using MarketplaceBetter.Domain.Entities.Advertising.Campaigns;
using MarketplaceBetter.Domain.Model.Advertising.Campaigns;

namespace MarketplaceBetter.Infrastructure.Mappings.Advertising.Campaigns
{
    public class AdProfile : Profile
    {
        public AdProfile()
        {
            CreateMap<Ad, AdModel>();
        }
    }
}
