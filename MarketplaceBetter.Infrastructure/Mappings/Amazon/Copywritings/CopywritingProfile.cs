using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Copywritings;
using MarketplaceBetter.Domain.Model.Amazon.Copywritings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.Copywritings
{
    public class CopywritingProfile : Profile
    {
        public CopywritingProfile()
        {
            CreateMap<Copywriting, CopywritingModel>();
        }
    }
}
