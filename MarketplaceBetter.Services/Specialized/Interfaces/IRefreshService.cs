using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface IRefreshService
    {
        event Action RefreshRequested;

        void RequestRefresh();
    }
}
