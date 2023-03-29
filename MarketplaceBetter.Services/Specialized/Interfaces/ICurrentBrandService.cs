using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface ICurrentBrandService
    {
        void SetCurrentBrand(BrandModel brand);

        Task<BrandModel> GetCurrentBrand();
    }
}
