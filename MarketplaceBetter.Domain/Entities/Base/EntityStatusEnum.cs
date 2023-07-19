using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Base
{
    public enum EntityStatusEnum
    {
        None = 0,

        Draft = 1,

        ToAdd = 2,

        Active = 3,

        ToUpdate = 4,

        ToWithdrawn = 5,

        Withdrawn = 6
    }
}
