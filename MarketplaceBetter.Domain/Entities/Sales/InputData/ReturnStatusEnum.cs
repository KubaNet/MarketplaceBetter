using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.InputData
{
    public enum ReturnStatusEnum
    {
        None = 0,

        UnitReturnedToInventory = 1,

        Reimbursed = 2,

        PendingRepackaging = 3,

        RepackagedSuccessfully = 4,
    }
}
