using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductVariant> _repository;
        private readonly IMapper _mapper;

        public ProductVariantService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ProductVariant>();
            _mapper = mapper;
        }

        public ProductVariantModel Get(long id)
        {
            ProductVariant variant = _repository.Get(id);

            return _mapper.Map<ProductVariantModel>(variant);
        }

        public IList<ProductVariantModel> GetAll()
        {
            IList<ProductVariant> variants = _repository.GetQuery().Include(v => v.Product).Include(v => v.Color).Include(v => v.Size).ToList();
            IList<ProductVariantModel> variantsModel = _mapper.Map<IList<ProductVariantModel>>(variants);

            return variantsModel;
        }

        public void Add(ProductVariantModel variant)
        {
            ProductVariant variantToAdd = new();

            TransferValues(variantToAdd, variant);

            _repository.Add(variantToAdd);
            _unitOfWork.Save();
        }

        public void Update(ProductVariantModel variant)
        {
            ProductVariant variantToUpdate = _repository.Get(variant.Id);

            TransferValues(variantToUpdate, variant);

            _repository.Update(variantToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(ProductVariant toProductVariant, ProductVariantModel fromProductVariant)
        {
            toProductVariant.Sku = fromProductVariant.Sku;
            toProductVariant.ProductId = fromProductVariant.Product.Id;
            toProductVariant.ColorId = fromProductVariant.Color.Id;
            toProductVariant.SizeId = fromProductVariant.Size.Id;
        }
    }
}
