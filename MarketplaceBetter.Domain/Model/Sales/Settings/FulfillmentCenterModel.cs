using MarketplaceBetter.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Settings
{
    public class FulfillmentCenterModel
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public CountryModel Country { get; set; }
    }
}
