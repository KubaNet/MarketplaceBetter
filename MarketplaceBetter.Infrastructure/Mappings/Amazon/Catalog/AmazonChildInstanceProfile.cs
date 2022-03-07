using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Catalog;
using MarketplaceBetter.Domain.Model.Amazon.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.Catalog
{
    public class AmazonChildInstanceProfile : Profile
    {
        public AmazonChildInstanceProfile()
        {
            CreateMap<AmazonChildInstance, AmazonChildInstanceModel>();
        }
    }
}
