using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Catalog
{
    public class SizeGroupProfile : Profile
    {
        public SizeGroupProfile()
        {
            CreateMap<SizeGroup, SizeGroupModel>();
        }
    }
}
