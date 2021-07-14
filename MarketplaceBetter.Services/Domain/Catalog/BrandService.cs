using AutoMapper;
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
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Brand>();
            _mapper = mapper;
        }

        public void Add(BrandModel brand)
        {
            Brand newBrand = new();

            TransferValues(newBrand, brand);

            _repository.Add(newBrand);
            _unitOfWork.Save();
        }

        public IList<BrandModel> GetBrands()
        {
            IList<BrandModel> brands = _mapper.Map<IList<BrandModel>>(_repository.GetAll());

            return brands;
        }

        private void TransferValues(Brand toBrand, BrandModel fromBrand)
        {
            toBrand.Name = fromBrand.Name;
            toBrand.Code = fromBrand.Code;
        }
    }
}
