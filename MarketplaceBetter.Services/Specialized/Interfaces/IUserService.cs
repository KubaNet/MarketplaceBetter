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
        void SetCurrentBrand(BrandModel brand);

        BrandModel GetCurrentBrand();

        bool IsSpecificBrand();

        void SetCurrentInstance(InstanceModel brand);

        InstanceModel GetCurrentInstance();

        bool IsSpecificInstance();

        bool IsExpanded(MenuItemEnum menu);

        void SetExpanded(MenuItemEnum menu, bool expanded);

        void SetShowDrafts(bool showDrafts);

        bool ShowDrafts();

        void SetShowWithdrawn(bool showWithdrawn);

        bool ShowWithdrawn();
    }
}
