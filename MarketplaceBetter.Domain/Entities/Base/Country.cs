using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Base
{
    public class Country : Entity
    {
        public string Name { get; set; }

        public CountryEnum SystemName { get; set; }

        public string Code { get; set; }
    }
}
