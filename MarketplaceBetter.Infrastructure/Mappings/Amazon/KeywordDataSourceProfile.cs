using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Amazon
{
    public class KeywordDataSourceProfile : Profile
    {
        public KeywordDataSourceProfile()
        {
            CreateMap<KeywordDataSource, KeywordDataSourceModel>();
        }
    }
}
