using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Settings.Interfaces
{
    public interface IShowDraftsSetting
    {
        void SetDraftsSetting(bool showDrafts);

        bool GetDraftsSetting();
    }
}
