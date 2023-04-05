using MarketplaceBetter.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base.Interfaces
{
    public interface IInstanceService
    {
        IList<InstanceModel> GetAll();

        IList<InstanceModel> GetAllNormal();

        IList<InstanceModel> GetAllNormal(bool onlyCurrent);
    }
}
