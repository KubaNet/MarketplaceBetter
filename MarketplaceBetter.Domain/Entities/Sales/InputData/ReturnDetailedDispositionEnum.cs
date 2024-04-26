using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.InputData
{
    public enum ReturnDetailedDispositionEnum
    {
        None = 0,

        SELLABLE = 1,

        DAMAGED = 2,

        CUSTOMER_DAMAGED = 3,

        DEFECTIVE = 4,

        CARRIER_DAMAGED = 5,

        EXPIRED = 6,
    }
}
