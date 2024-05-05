using AutoMapper;
using MarketplaceBetter.Domain.Entities.Advertising.Campaigns;
using MarketplaceBetter.Domain.Model.Advertising.Campaigns;

namespace MarketplaceBetter.Infrastructure.Mappings.Advertising.Campaigns
{
    public class CampaignProfile : Profile
    {
        public CampaignProfile()
        {
            CreateMap<Campaign, CampaignModel>();
        }
    }
}
