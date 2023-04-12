using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Settings.Interfaces
{
    public interface IShowWithdrawnSetting
    {
        void SetWithdrawnSetting(bool showWithdrawn);

        bool ShowWithdrawn();
    }
}
