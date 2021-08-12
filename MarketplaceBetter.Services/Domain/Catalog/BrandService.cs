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

        public BrandService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Brand>();
            _mapper = mapper;
        }

        public BrandModel Get(long id) => _mapper.Map<BrandModel>(_repository.Get(id));

        public IList<BrandModel> GetAll() => _mapper.Map<IList<BrandModel>>(_repository.GetAll().OrderBy(b => b.Name));

        public void Add(BrandModel brand)
        {
            Brand brandToAdd = new();

            TransferValues(brandToAdd, brand);

            _repository.Add(brandToAdd);
            _unitOfWork.Save();
        }

        public void Update(BrandModel brand)
        {
            Brand brandToUpdate = _repository.Get(brand.Id);

            TransferValues(brandToUpdate, brand);

            _repository.Update(brandToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Brand toBrand, BrandModel fromBrand)
        {
            toBrand.Name = fromBrand.Name;
            toBrand.Code = fromBrand.Code;
        }
    }
}
