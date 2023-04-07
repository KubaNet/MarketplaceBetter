using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Settings.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Settings
{
    internal class ExpandedMenuSetting : IExpandedMenuSetting
    {
        private const string STORAGE_KEY = "ExpandedMenu";
        private readonly IConfiguration _configuration;
        private IList<MenuItemEnum> _menuItemsCache = new List<MenuItemEnum>();

        public ExpandedMenuSetting(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsExpanded(MenuItemEnum menu)
        {
            if (!_menuItemsCache.Any())
            {
                _menuItemsCache = GetExpandedMenuItems();
            }

            return _menuItemsCache.Contains(menu);
        }

        public void SetExpanded(MenuItemEnum menu, bool expanded)
        {
            IList<MenuItemEnum> menuItems = GetExpandedMenuItems();

            if (expanded)
            {
                if (menuItems.Contains(menu))
                {
                    return;
                }
                else
                {
                    menuItems.Add(menu);
                }
            }
            else
            {
                if (menuItems.Contains(menu))
                {
                    menuItems.Remove(menu);
                }
                else
                {
                    return;
                }
            }

            SaveExpandedMenuItems(menuItems);
        }

        private IList<MenuItemEnum> GetExpandedMenuItems()
        {
            IList<MenuItemEnum> menuItems = new List<MenuItemEnum>();

            IList<string> menuItemsStrings = _configuration.GetValue<string>(STORAGE_KEY).Split(';');
            foreach (string menuItemString in menuItemsStrings)
            {
                if (string.IsNullOrWhiteSpace(menuItemString))
                {
                    continue;
                }

                MenuItemEnum menuItem = Enum.Parse<MenuItemEnum>(menuItemString);

                menuItems.Add(menuItem);
            }

            return menuItems;
        }

        private void SaveExpandedMenuItems(IList<MenuItemEnum> menuItems)
        {
            _configuration[STORAGE_KEY] = string.Join(";", menuItems.Cast<int>());

            _menuItemsCache.Clear();
        }
    }
}
