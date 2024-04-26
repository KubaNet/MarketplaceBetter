using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.InputData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Mappings.Sales.InputData
{
    public class FulfilledShipmentProfile : Profile
    {
        public FulfilledShipmentProfile()
        {
            CreateMap<FulfilledShipment, FulfilledShipmentModel>();
        }
    }
}
