using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base.Interfaces
{
    public interface IUserService
    {
        IList<UserModel> GetAll();

        bool IsCorrectPassword(string login, string password);

        void Login(string login);

        void Logout();

        bool LoginExpired();

        bool SettingsChanged();

        string GetCurrentUserLogin();

        BrandModel GetCurrentBrand();

        void SetCurrentBrand(BrandModel brand);

        bool IsSpecificBrand();

        InstanceModel GetCurrentInstance();

        void SetCurrentInstance(InstanceModel instance);

        bool IsSpecificInstance();

        bool IsExpanded(MenuItemEnum menu);

        void SetExpanded(MenuItemEnum menu, bool expanded);

        bool ShowDrafts();

        void SetShowDrafts(bool showDrafts);

        bool ShowWithdrawn();

        void SetShowWithdrawn(bool showWithdrawn);
    }
}
