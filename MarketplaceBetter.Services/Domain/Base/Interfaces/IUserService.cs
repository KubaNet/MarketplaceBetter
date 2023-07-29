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

        EntityStatusModel GetCurrentStatus();

        void SetCurrentStatus(EntityStatusModel status);

        bool IsSpecificStatus();

        BrandModel GetCurrentBrand();

        void SetCurrentBrand(BrandModel brand);

        bool IsSpecificBrand();

        CollectionModel GetCurrentCollection();

        void SetCurrentCollection(CollectionModel collection);

        bool IsSpecificCollection();

        InstanceModel GetCurrentInstance();

        void SetCurrentInstance(InstanceModel instance);

        bool IsSpecificInstance();

        bool IsExpanded(MenuItemEnum menu);

        void SetExpanded(MenuItemEnum menu, bool expanded);

        bool HideDrafts();

        void SetHideDrafts(bool hideDrafts);

        bool HideWithdrawn();

        void SetHideWithdrawn(bool hideWithdrawn);
    }
}
