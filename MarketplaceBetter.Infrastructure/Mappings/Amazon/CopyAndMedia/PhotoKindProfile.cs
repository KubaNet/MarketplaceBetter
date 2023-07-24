using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon.CopyAndMedia
{
    public class PhotoKindProfile : Profile
    {
        public PhotoKindProfile()
        {
            CreateMap<PhotoKind, PhotoKindModel>();
        }
    }
}
