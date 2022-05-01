using MarketplaceBetter.Domain.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Interfaces
{
    public interface IInstanceService
    {
        IList<InstanceModel> GetAll();
    }
}
