using MarketplaceBetter.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.Settings
{
    public class FulfillmentCenter : Entity
    {
        public string Code { get; set; }

        public long CountryId { get; set; }

        public Country Country { get; set; }
    }
}
