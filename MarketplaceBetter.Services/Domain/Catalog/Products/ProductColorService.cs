using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
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
using Color = MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes.Color;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

namespace MarketplaceBetter.Services.Domain.Catalog.Products
{
    public class ProductColorService : IProductColorService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Variant> _repository;
		private readonly ICurrentBrandService _currentBrandService;

		public ProductColorService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICurrentBrandService currentBrandService)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<Variant>();
            _currentBrandService = currentBrandService;
		}

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Variant> variants = _repository.GetQuery();

            variants = ApplyFilter(variants, request);

            var groupedVariants = variants.GroupBy(v => new { v.ProductId, v.ColorId, v.StatusId });

            return groupedVariants.Count();
        }

        public IList<ProductColorModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Variant> variants = _repository.GetQuery();

            variants = ApplyFilter(variants, request);
            variants = ApplySorting(variants, request);

            var groupedVariants = variants.ToList().GroupBy(v => new { v.ProductId, v.ColorId, v.StatusId });

            if (!request.ShowAll)
            {
                groupedVariants = groupedVariants.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }

            return groupedVariants.Select(v => new ProductColorModel
            {
                Product = _mapper.Map<ProductModel>(v.First().Product),
                Color = _mapper.Map<ColorModel>(v.First().Color),
                Status = _mapper.Map<VariantStatusModel>(v.First().Status),
                Sizes = _mapper.Map<IList<SizeModel>>(v.Select(g => g.Size))
            }).ToList();
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
                string[] searchFieldNames = new[] { "product", "productcode", "color", "colorcode", "status", "sizes", "brand", "product_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    variants = searchField.Name switch
                    {
                        "product" => variants.Where(v => v.Product.Name.Contains(searchField.Value)),
                        "productcode" => variants.Where(v => v.Product.Code.Contains(searchField.Value)),
                        "status" => variants.Where(v => v.Status.Name.Contains(searchField.Value)),
                        "color" => variants.Where(v => v.Color.Name.Contains(searchField.Value)),
                        "colorcode" => variants.Where(v => v.Color.Code.Contains(searchField.Value)),
                        "sizes" => variants.Where(v => v.Size.Name.Contains(searchField.Value)),
                        "brand" => variants.Where(v => v.Product.Brand.Name.Contains(searchField.Value)),
                        "product_id" => variants.Where(v => v.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    variants = variants.Where(v => v.Product.Name.Contains(searchString)
                        || v.Product.Code.Contains(searchString)
                        || v.Status.Name.Contains(searchString)
                        || v.Color.Name.Contains(searchString)
                        || v.Color.Code.Contains(searchString)
                        || v.Size.Name.Contains(searchString)
                        || v.Product.Brand.Name.Contains(searchString));
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
                    "product" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Name) : variants.OrderByDescending(v => v.Product.Name),
                    "productcode" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Code) : variants.OrderByDescending(v => v.Product.Code),
                    "status" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Status.Name) : variants.OrderByDescending(v => v.Status.Name),
                    "color" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Color.Name) : variants.OrderByDescending(v => v.Color.Name),
                    "colorcode" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Color.Code) : variants.OrderByDescending(v => v.Color.Code),
                    "sizes" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Size.Name) : variants.OrderByDescending(v => v.Size.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Brand.Name) : variants.OrderByDescending(v => v.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                variants = variants.OrderBy(v => v.Product.Id);
            }

            return variants;
        }
    }
}
