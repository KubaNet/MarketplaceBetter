using AutoMapper;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
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
        private readonly IAmazonChildService _amazonChildService;

        public VariantService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IAmazonChildService amazonChildService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Variant>();
            _amazonChildService = amazonChildService;
        }

        public VariantModel Get(long id) => _mapper.Map<VariantModel>(_repository.Get(id));

        public IList<VariantModel> GetAll() => _mapper.Map<IList<VariantModel>>(_repository.GetQuery().OrderBy(g => g.Sku));

        public IList<VariantModel> GetAllForProduct(long productId) => _mapper.Map<IList<VariantModel>>(_repository.GetQuery().Where(v => v.ProductId == productId));

        public int CountForListRequest(ListRequest request) => _repository.GetQuery().Count();

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

            _repository.Add(variantToAdd);
            _unitOfWork.Save();

            _amazonChildService.AddForVariant(variantToAdd.Id);
        }

        public void Update(VariantModel variant)
        {
            Variant variantToUpdate = _repository.Get(variant.Id);

            TransferValues(variantToUpdate, variant);

            _repository.Update(variantToUpdate);
            _unitOfWork.Save();

            _amazonChildService.AddForVariant(variantToUpdate.Id);
        }

        private void TransferValues(Variant toVariant, VariantModel fromVariant)
        {
            toVariant.Sku = fromVariant.Sku;
            toVariant.StatusId = fromVariant.Status.Id;
            toVariant.ProductId = fromVariant.Product.Id;
            toVariant.ColorId = fromVariant.Color.Id;
            toVariant.SizeId = fromVariant.Size.Id;
        }

        private IQueryable<Variant> ApplyFilter(IQueryable<Variant> variants, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return variants;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "status", "product", "brand", "color", "size" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    variants = searchField.Name switch
                    {
                        "id" => variants.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => variants.Where(p => p.Sku.Contains(searchField.Value)),
                        "status" => variants.Where(p => p.Status.Name.Contains(searchField.Value)),
                        "product" => variants.Where(p => p.Product.Name.Contains(searchField.Value)),
                        "brand" => variants.Where(p => p.Product.Brand.Name.Contains(searchField.Value)),
                        "color" => variants.Where(p => p.Color.Name.Contains(searchField.Value)),
                        "size" => variants.Where(p => p.Size.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    variants = variants.Where(p => p.Id == searchString.ParseToIntOrDefault()
                      || p.Sku.Contains(searchString)
                      || p.Status.Name.Contains(searchString)
                      || p.Product.Name.Contains(searchString)
                      || p.Product.Brand.Name.Contains(searchString)
                      || p.Color.Name.Contains(searchString)
                      || p.Size.Name.Contains(searchString));
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
                    "brand" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Brand.Name) : variants.OrderByDescending(v => v.Product.Brand.Name),
                    "color" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Color.Name) : variants.OrderByDescending(v => v.Color.Name),
                    "size" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Size.Name) : variants.OrderByDescending(v => v.Size.Name),
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
            return variants.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
