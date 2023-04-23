using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.APlusContents
{
    public class APlusContentProfile : Profile
    {
        public APlusContentProfile()
        {
            CreateMap<APlusContent, APlusContentModel>();
        }
    }
}
