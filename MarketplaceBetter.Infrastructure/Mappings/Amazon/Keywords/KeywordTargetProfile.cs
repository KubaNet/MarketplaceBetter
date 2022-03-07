using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon
{
    public class KeywordTargetProfile : Profile
    {
        public KeywordTargetProfile()
        {
            CreateMap<KeywordTarget, KeywordTargetModel>();
        }
    }
}
