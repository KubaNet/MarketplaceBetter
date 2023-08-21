using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Base
{
    public class Currency : Entity
    {
        public string Name { get; set; }

        public CurrencyEnum SystemName { get; set; }
    }
}
