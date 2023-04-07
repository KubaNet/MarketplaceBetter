using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Settings.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Settings
{
    public class CurrentBrandSetting : ICurrentBrandSetting
    {
        private const string STORAGE_KEY = "CurrentBrand";
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepository<Brand> _brandRepository;

        public CurrentBrandSetting(
            IConfiguration configuration,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _brandRepository = unitOfWork.GetRepository<Brand>();
        }

        public BrandModel GetCurrentBrand()
        {
            string brandName = _configuration.GetValue<string>(STORAGE_KEY);

            if (brandName.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return _mapper.Map<BrandModel>(_brandRepository.Single(b => b.Name == brandName));
        }

        public void SetCurrentBrand(BrandModel brand)
        {
            _configuration[STORAGE_KEY] = brand.Name;
        }

        public bool IsSpecificBrand()
        {
            BrandModel brand = GetCurrentBrand();

            return brand != null && !brand.Name.Equals("All", StringComparison.OrdinalIgnoreCase);
        }
    }
}
