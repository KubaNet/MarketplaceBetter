using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Catalog.ColorsAndSizes
{
    public class ColorGroupProfile : Profile
    {
        public ColorGroupProfile()
        {
            CreateMap<ColorGroup, ColorGroupModel>();
        }
    }
}
