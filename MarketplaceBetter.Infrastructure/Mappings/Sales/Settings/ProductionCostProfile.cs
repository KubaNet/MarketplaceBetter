using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Sales.Settings
{
    public class ProductionCostProfile : Profile
    {
        public ProductionCostProfile()
        {
            CreateMap<ProductionCost, ProductionCostModel>();
        }
    }
}
