using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Catalog.CopyAndMedia
{
    public class PhotoUploadProfile : Profile
    {
        public PhotoUploadProfile()
        {
            CreateMap<PhotoUpload, PhotoUploadModel>();
        }
    }
}
