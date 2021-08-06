using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using MarketplaceBetter.Services.Model.ListRequests.Catalog;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
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
            return _mapper.Map<ProductVariantModel>(_repository.Get(id));
        }

        public IList<ProductVariantModel> GetAll()
        {
            return _mapper.Map<IList<ProductVariantModel>>(_repository.GetAll());
        }

        public IList<ProductVariantModel> GetForProduct(long productId)
        {
            return _mapper.Map<IList<ProductVariantModel>>(_repository.GetQuery().Where(v => v.ProductId == productId));
        }

        public int CountForListRequest(ProductVariantListRequest request)
        {
            return _repository.GetQuery().Count();
        }

        public IList<ProductVariantModel> GetForListRequest(ProductVariantListRequest request)
        {
            IQueryable<ProductVariant> variants = _repository.GetQuery();

            ApplyFilter(variants, request);
            ApplySorting(variants, request);
            ApplyPaging(variants, request);

            return _mapper.Map<IList<ProductVariantModel>>(variants);
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

        private void ApplyFilter(IQueryable<ProductVariant> variants, ProductVariantListRequest request)
        {
            if (request.Id.HasValue)
            {
                variants = variants.Where(p => p.Id == request.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Sku))
            {
                variants = variants.Where(p => p.Sku.Contains(request.Sku));
            }

            if (request.Product != null)
            {
                variants = variants.Where(p => p.ProductId == request.Product.Id);
            }

            if (request.Color != null)
            {
                variants = variants.Where(p => p.ColorId == request.Color.Id);
            }

            if (request.Size != null)
            {
                variants = variants.Where(p => p.SizeId == request.Size.Id);
            }
        }

        private void ApplySorting(IQueryable<ProductVariant> variants, ProductVariantListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                variants = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Id) : variants.OrderByDescending(v => v.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Sku) : variants.OrderByDescending(v => v.Sku),
                    "product" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Name) : variants.OrderByDescending(v => v.Product.Name),
                    "color" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Color.Name) : variants.OrderByDescending(v => v.Color.Name),
                    "size" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Size.Name) : variants.OrderByDescending(v => v.Size.Name),
                    _ => throw new UnrecognizedSortingException<ProductVariantListRequest>(request.SortBy)
                };
            }
        }

        private void ApplyPaging(IQueryable<ProductVariant> variants, ProductVariantListRequest request)
        {
            variants = variants.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
