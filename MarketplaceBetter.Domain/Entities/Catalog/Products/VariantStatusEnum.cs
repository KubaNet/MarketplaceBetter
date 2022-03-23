using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.Products
{
    public enum VariantStatusEnum
    {
        None = 0,

        Active = 1,

        Withdrawn = 2,

        ToWithdrawn = 3,

        ToAdd = 4
    }
}
