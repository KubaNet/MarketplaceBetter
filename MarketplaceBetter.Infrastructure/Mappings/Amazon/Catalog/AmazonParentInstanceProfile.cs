using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.Inventory
{
    public class AmazonParentInstanceProfile : Profile
    {
        public AmazonParentInstanceProfile()
        {
            CreateMap<AmazonParentInstance, AmazonParentInstanceModel>();
        }
    }
}
