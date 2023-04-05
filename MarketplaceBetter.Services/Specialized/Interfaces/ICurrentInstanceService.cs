using MarketplaceBetter.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface ICurrentInstanceService
    {
        void SetCurrentInstance(InstanceModel brand);

        InstanceModel GetCurrentInstance();

        bool IsSpecificInstance();
    }
}
