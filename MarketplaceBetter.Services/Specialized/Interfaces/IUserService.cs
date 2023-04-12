using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface IUserService
    {
        bool IsCorrectPassword(string login, string password);

        BrandModel GetCurrentBrand();

        void SetCurrentBrand(BrandModel brand);

        bool IsSpecificBrand();

        InstanceModel GetCurrentInstance();

        void SetCurrentInstance(InstanceModel brand);

        bool IsSpecificInstance();

        bool IsExpanded(MenuItemEnum menu);

        void SetExpanded(MenuItemEnum menu, bool expanded);

        bool ShowDrafts();

        void SetShowDrafts(bool showDrafts);

        bool ShowWithdrawn();

        void SetShowWithdrawn(bool showWithdrawn);
    }
}
