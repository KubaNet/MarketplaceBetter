using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products
{
    public class BrandService : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Brand> _repository;
        private readonly IUserService _userService;

        public BrandService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Brand>();
            _userService = userService;
        }

        public BrandModel Get(long id) => _mapper.Map<BrandModel>(_repository.Get(id));

        public BrandModel GetByName(string name) => _mapper.Map<BrandModel>(_repository.Single(b => b.Name == name));

        public IList<BrandModel> GetAll() => GetAll(false);

        public IList<BrandModel> GetAll(bool onlyCurrent)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (onlyCurrent && _userService.IsSpecificBrand() && currentBrand != null)
            {
                return _mapper.Map<IList<BrandModel>>(_repository.Where(b => b.Id == currentBrand.Id));
            }
            else
            {
                return _mapper.Map<IList<BrandModel>>(_repository.GetQuery().OrderBy(b => b.Name));
            }
        }

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
