using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.Attributes;
using MarketplaceBetter.Domain.Model.Catalog.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Catalog.Attributes
{
    public class ProductDimensionsProfile : Profile
    {
        public ProductDimensionsProfile()
        {
            CreateMap<ProductDimensions, ProductDimensionsModel>();
        }
    }
}
