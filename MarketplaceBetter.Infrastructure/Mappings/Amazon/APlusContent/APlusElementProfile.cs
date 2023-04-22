using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContent;
using MarketplaceBetter.Domain.Model.Amazon.APlusContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.APlusContent
{
    public class APlusElementProfile : Profile
    {
        public APlusElementProfile()
        {
            CreateMap<APlusElement, APlusElementModel>();
        }
    }
}
