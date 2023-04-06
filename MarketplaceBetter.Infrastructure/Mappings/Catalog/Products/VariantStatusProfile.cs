using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Catalog.Products
{
    public class EntityStatusProfile : Profile
    {
        public EntityStatusProfile()
        {
            CreateMap<EntityStatus, EntityStatusModel>();
        }
    }
}
