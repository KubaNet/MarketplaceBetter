using MarketplaceBetter.Services.Specialized.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class RefreshService : IRefreshService
    {
        public event Action RefreshRequested;

        public void RequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
