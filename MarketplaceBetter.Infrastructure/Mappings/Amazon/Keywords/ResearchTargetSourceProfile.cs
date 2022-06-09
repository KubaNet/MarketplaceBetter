using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.Keywords
{
    public class ResearchTargetSourceProfile : Profile
    {
        public ResearchTargetSourceProfile()
        {
            CreateMap<ResearchTargetSource, ResearchTargetSourceModel>();
        }
    }
}
