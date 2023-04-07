using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Settings.Interfaces
{
    public interface IExpandedMenuSetting
    {
        bool IsExpanded(MenuItemEnum menu);

        void SetExpanded(MenuItemEnum menu, bool expanded);
    }
}
