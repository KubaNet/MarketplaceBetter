using Blazored.SessionStorage;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using MarketplaceBetter.Services.Specialized.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class CurrentBrandService : ICurrentBrandService
    {
        private const string STORAGE_KEY = "CurrentBrand";
        private readonly ISessionStorageService _sessionStorageService;
        private readonly IBrandService _brandService;

        public CurrentBrandService(ISessionStorageService sessionStorageService,
            IBrandService brandService)
        {
            _sessionStorageService = sessionStorageService;
            _brandService = brandService;
        }

        public async Task<BrandModel> GetCurrentBrand()
        {
            string brandName = await _sessionStorageService.GetItemAsStringAsync(STORAGE_KEY);

            return _brandService.GetByName(brandName);
        }

        public void SetCurrentBrand(BrandModel brand)
        {
            _sessionStorageService.SetItemAsStringAsync(STORAGE_KEY, brand.Name);
        }
    }
}
