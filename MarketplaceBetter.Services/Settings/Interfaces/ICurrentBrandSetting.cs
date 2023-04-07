using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Settings.Interfaces
{
    public interface ICurrentBrandSetting
    {
        void SetCurrentBrand(BrandModel brand);

        BrandModel GetCurrentBrand();

        bool IsSpecificBrand();
    }
}
