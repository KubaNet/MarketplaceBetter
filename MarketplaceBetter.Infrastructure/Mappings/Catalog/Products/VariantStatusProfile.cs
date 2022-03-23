using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Catalog.Products
{
    public class VariantStatusProfile : Profile
    {
        public VariantStatusProfile()
        {
            CreateMap<VariantStatus, VariantStatusModel>();
        }
    }
}
