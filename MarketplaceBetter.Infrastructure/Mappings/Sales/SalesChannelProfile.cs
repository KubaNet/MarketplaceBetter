using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Sales
{
    public class SalesChannelProfile : Profile
    {
        public SalesChannelProfile()
        {
            CreateMap<SalesChannel, SalesChannelModel>();
        }
    }
}
