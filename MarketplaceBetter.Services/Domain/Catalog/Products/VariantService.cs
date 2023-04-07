using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

namespace MarketplaceBetter.Services.Domain.Catalog.Products
{
    public class VariantService : IVariantService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Variant> _repository;
        private readonly IRepository<EntityStatus> _statusRepository;
        private readonly IChildInstanceService _childInstanceService;
        private readonly IPhotoService _photoService;
        private readonly IPhotoUploadService _photoUploadService;
        private readonly ICurrentBrandService _currentBrandService;

        public VariantService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IChildInstanceService childInstanceService,
            IPhotoService photoService,
            IPhotoUploadService photoUploadService,
            ICurrentBrandService currentBrandService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Variant>();
            _statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _childInstanceService = childInstanceService;
            _photoService = photoService;
            _photoUploadService = photoUploadService;
            _currentBrandService = currentBrandService;
        }

        public VariantModel Get(long id) => _mapper.Map<VariantModel>(_repository.Get(id));

        public IList<VariantModel> GetAll()
        {
            if (_currentBrandService.IsSpecificBrand())
            {
                BrandModel currentBrand = _currentBrandService.GetCurrentBrand();

                return _mapper.Map<IList<VariantModel>>(_repository.Where(v => v.Product.BrandId == currentBrand.Id).OrderBy(v => v.Sku));
            }
            else
            {
                return _mapper.Map<IList<VariantModel>>(_repository.GetQuery().OrderBy(v => v.Sku));
            }
        }

        public IList<VariantModel> GetAllForProduct(long productId) => _mapper.Map<IList<VariantModel>>(_repository.GetQuery().Where(v => v.ProductId == productId));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Variant> variants = _repository.GetQuery();

            variants = ApplyFilter(variants, request);

            return variants.Count();
        }

        public IList<VariantModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Variant> variants = _repository.GetQuery();

            variants = ApplyFilter(variants, request);
            variants = ApplySorting(variants, request);
            variants = ApplyPaging(variants, request);

            return _mapper.Map<IList<VariantModel>>(variants);
        }

        public void Add(VariantModel variant)
        {
            Variant variantToAdd = new();

            TransferValues(variantToAdd, variant);
            variantToAdd.Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft);

            _repository.Add(variantToAdd);
            _unitOfWork.Save();

            _childInstanceService.AddForVariant(variantToAdd.Id);
        }

        public void Update(VariantModel variant)
        {
            Variant variantToUpdate = _repository.Get(variant.Id);

            TransferValues(variantToUpdate, variant);

            _repository.Update(variantToUpdate);
            _unitOfWork.Save();

            _childInstanceService.AddForVariant(variantToUpdate.Id);
        }

        public void Delete(long id)
        {
            _childInstanceService.DeleteAllForVariant(id);
            _photoService.DeleteAllForVariant(id);
            _photoUploadService.RemoveAllForVariant(id);

            Variant variant = _repository.Get(id);

            _repository.Delete(variant);
            _unitOfWork.Save();
        }

        private void TransferValues(Variant toVariant, VariantModel fromVariant)
        {
            toVariant.Sku = fromVariant.Sku;
            toVariant.ProductId = fromVariant.Product.Id;
            toVariant.ColorId = fromVariant.Color.Id;
            toVariant.SizeId = fromVariant.Size.Id;
            toVariant.Ean = fromVariant.Ean;
            toVariant.Asin = fromVariant.Asin;
        }

        private IQueryable<Variant> ApplyFilter(IQueryable<Variant> variants, ListRequest request)
        {
            if (_currentBrandService.IsSpecificBrand())
            {
                variants = variants.Where(v => v.Product.BrandId == _currentBrandService.GetCurrentBrand().Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return variants;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "status", "product", "product_code", "brand", "color", "size", "ean", "asin", "product_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    variants = searchField.Name switch
                    {
                        "id" => variants.Where(v => v.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => variants.Where(v => v.Sku.Contains(searchField.Value)),
                        "status" => variants.Where(v => v.Status.Name.Contains(searchField.Value)),
                        "product" => variants.Where(v => v.Product.Name.Contains(searchField.Value)),
                        "product_code" => variants.Where(v => v.Product.Code.Contains(searchField.Value)),
                        "brand" => variants.Where(v => v.Product.Brand.Name.Contains(searchField.Value)),
                        "color" => variants.Where(v => v.Color.Name.Contains(searchField.Value)),
                        "size" => variants.Where(v => v.Size.Name.Contains(searchField.Value)),
                        "ean" => variants.Where(v => v.Ean.Contains(searchField.Value)),
                        "asin" => variants.Where(v => v.Asin.Contains(searchField.Value)),
                        "product_id" => variants.Where(v => v.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    variants = variants.Where(v => v.Id == searchString.ParseToIntOrDefault()
                      || v.Sku.Contains(searchString)
                      || v.Status.Name.Contains(searchString)
                      || v.Product.Name.Contains(searchString)
                      || v.Product.Code.Contains(searchString)
                      || v.Product.Brand.Name.Contains(searchString)
                      || v.Color.Name.Contains(searchString)
                      || v.Size.Name.Contains(searchString)
                      || v.Ean.Contains(searchString)
                      || v.Asin.Contains(searchString));
                }
            }

            return variants;
        }

        private IQueryable<Variant> ApplySorting(IQueryable<Variant> variants, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                variants = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Id) : variants.OrderByDescending(v => v.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Sku) : variants.OrderByDescending(v => v.Sku),
                    "status" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Status.Name) : variants.OrderByDescending(v => v.Status.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Name) : variants.OrderByDescending(v => v.Product.Name),
                    "product_code" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Code) : variants.OrderByDescending(v => v.Product.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Brand.Name) : variants.OrderByDescending(v => v.Product.Brand.Name),
                    "color" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Color.Name) : variants.OrderByDescending(v => v.Color.Name),
                    "size" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Size.Name) : variants.OrderByDescending(v => v.Size.Name),
                    "ean" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Ean) : variants.OrderByDescending(v => v.Ean),
                    "asin" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Asin) : variants.OrderByDescending(v => v.Asin),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                variants = variants.OrderBy(v => v.Id);
            }

            return variants;
        }

        private IQueryable<Variant> ApplyPaging(IQueryable<Variant> variants, ListRequest request)
        {
            return request.ShowAll ? variants : variants.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
