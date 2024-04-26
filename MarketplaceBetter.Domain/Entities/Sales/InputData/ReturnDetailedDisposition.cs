using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.InputData
{
    public class ReturnDetailedDisposition : Entity
    {
        public string Name { get; set; }

        public ReturnDetailedDispositionEnum SystemName { get; set; }
    }
}
