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

        public ProductVariantModel Get(long id) => _mapper.Map<ProductVariantModel>(_repository.Get(id));

        public IList<ProductVariantModel> GetAll() => _mapper.Map<IList<ProductVariantModel>>(_repository.GetAll().OrderBy(g => g.Sku));

        public IList<ProductVariantModel> GetForProduct(long productId) => _mapper.Map<IList<ProductVariantModel>>(_repository.GetQuery().Where(v => v.ProductId == productId));

        public int CountForListRequest(ProductVariantListRequest request) => _repository.GetQuery().Count();

        public IList<ProductVariantModel> GetForListRequest(ProductVariantListRequest request)
        {
            IQueryable<ProductVariant> variants = _repository.GetQuery();

            variants = ApplyFilter(variants, request);
            variants = ApplySorting(variants, request);
            variants = ApplyPaging(variants, request);

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

        private void TransferValues(ProductVariant toVariant, ProductVariantModel fromVariant)
        {
            toVariant.Sku = fromVariant.Sku;
            toVariant.ProductId = fromVariant.Product.Id;
            toVariant.ColorId = fromVariant.Color.Id;
            toVariant.SizeId = fromVariant.Size.Id;
        }

        private IQueryable<ProductVariant> ApplyFilter(IQueryable<ProductVariant> variants, ProductVariantListRequest request)
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

            return variants;
        }

        private IQueryable<ProductVariant> ApplySorting(IQueryable<ProductVariant> variants, ProductVariantListRequest request)
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

            return variants;
        }

        private IQueryable<ProductVariant> ApplyPaging(IQueryable<ProductVariant> variants, ProductVariantListRequest request)
        {
            return variants.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
