using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.Campaigns.Campaigns
{
    public class CampaignTypeProfile : Profile
    {
        public CampaignTypeProfile()
        {
            CreateMap<CampaignType, CampaignTypeModel>();
        }
    }
}
