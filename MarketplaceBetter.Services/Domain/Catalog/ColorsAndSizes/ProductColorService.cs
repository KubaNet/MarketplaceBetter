using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes
{
    public class ProductColorService : IProductColorService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Variant> _repository;
        private readonly IUserService _userService;

        public ProductColorService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<Variant>();
            _userService = userService;
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
                Status = _mapper.Map<EntityStatusModel>(v.First().Status),
                Sizes = _mapper.Map<IList<SizeModel>>(v.Select(g => g.Size))
            }).ToList();
        }

        public IList<ColorModel> GetAllForProduct(long productId)
        {
            IQueryable<Variant> variants = _repository.Where(v => v.ProductId == productId);

            return _mapper.Map<IList<ColorModel>>(variants.Select(v => v.Color).Distinct());
        }

        private IQueryable<Variant> ApplyFilter(IQueryable<Variant> variants, ListRequest request)
        {
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                variants = variants.Where(v => v.Product.BrandId == currentBrand.Id);
            }

            if (_userService.IsSpecificCollection())
            {
                CollectionModel currentCollection = _userService.GetCurrentCollection();
                variants = variants.Where(v => v.Product.CollectionId == currentCollection.Id);
            }

            if (_userService.IsSpecificSize())
            {
                StandardSizeModel currentSize = _userService.GetCurrentSize();
                if (currentSize.SystemName == StandardSizeEnum.OneSizePlusM)
                {
                    variants = variants.Where(v => v.Size.StandardSize.SystemName == StandardSizeEnum.OneSize || v.Size.StandardSize.SystemName == StandardSizeEnum.M);
                }
                else
                {
                    variants = variants.Where(v => v.Size.StandardSizeId == currentSize.Id);
                }
            }

            if (_userService.IsSpecificStatus())
            {
                EntityStatusModel currentStatus = _userService.GetCurrentStatus();
                variants = variants.Where(v => v.StatusId == currentStatus.Id);
            }

            bool hideDrafts = _userService.HideDrafts();
            if (hideDrafts && !_userService.IsSpecificStatus())
            {
                variants = variants.Where(v => v.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool hideWithdrawn = _userService.HideWithdrawn();
            if (hideWithdrawn && !_userService.IsSpecificStatus())
            {
                variants = variants.Where(v => v.Status.SystemName != EntityStatusEnum.Withdrawn);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return variants;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "product", "product_id", "product_code", "color", "color_code", "status", "sizes", "brand" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    variants = searchField.Name switch
                    {
                        "product" => variants.Where(v => v.Product.Name.Contains(searchField.Value)),
                        "product_id" => variants.Where(v => v.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        "product_code" => variants.Where(v => v.Product.Code.Contains(searchField.Value)),
                        "status" => variants.Where(v => v.Status.Name.Contains(searchField.Value)),
                        "color" => variants.Where(v => v.Color.Name.Contains(searchField.Value)),
                        "color_code" => variants.Where(v => v.Color.Code.Contains(searchField.Value)),
                        "sizes" => variants.Where(v => v.Size.Name.Contains(searchField.Value)),
                        "brand" => variants.Where(v => v.Product.Brand.Name.Contains(searchField.Value)),
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
                    "product_code" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Product.Code) : variants.OrderByDescending(v => v.Product.Code),
                    "status" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Status.Name) : variants.OrderByDescending(v => v.Status.Name),
                    "color" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Color.Name) : variants.OrderByDescending(v => v.Color.Name),
                    "color_code" => request.SortDirection == SortDirection.Ascending ? variants.OrderBy(v => v.Color.Code) : variants.OrderByDescending(v => v.Color.Code),
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
