using MarketplaceBetter.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Base
{
    public class CountryModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public CountryEnum SystemName { get; set; }

        public string Code { get; set; }
    }
}
