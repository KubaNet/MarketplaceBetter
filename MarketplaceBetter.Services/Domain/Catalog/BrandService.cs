using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Brand> _repository;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Brand>();
        }

        public void Add(BrandModel brand)
        {
            Brand newBrand = new();

            TransferValues(brand, newBrand);

            _repository.Add(newBrand);
            _unitOfWork.Save();
        }

        public IList<BrandModel> GetBrands()
        {
            IList<BrandModel> brands = new List<BrandModel> { new BrandModel { Id = 1, Name = "Futrzane", Code = "FUT" } };

            return brands;
        }

        private void TransferValues(BrandModel fromBrand, Brand toBrand)
        {
            toBrand.Name = fromBrand.Name;
            toBrand.Code = fromBrand.Code;
        }
    }
}
