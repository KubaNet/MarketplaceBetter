using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.Attributes;
using MarketplaceBetter.Domain.Model.Catalog.Attributes;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.Attributes.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Attributes
{
    public class ProductDimensionsService : IProductDimensionsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductDimensions> _repository;
        private readonly IUserService _userService;

        public ProductDimensionsService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ProductDimensions>();
            _userService = userService;
        }

        public ProductDimensionsModel Get(long id) => _mapper.Map<ProductDimensionsModel>(_repository.Get(id));

        public IList<ProductDimensionsModel> GetAll()
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (currentBrand != null && _userService.IsSpecificBrand())
            {
                return _mapper.Map<IList<ProductDimensionsModel>>(_repository.Where(d => d.Product.BrandId == currentBrand.Id).OrderBy(d => d.Id));
            }
            else
            {
                return _mapper.Map<IList<ProductDimensionsModel>>(_repository.GetQuery().OrderBy(d => d.Id));
            }
        }

        public IList<ProductDimensionsModel> GetAllForProduct(long productId) => _mapper.Map<IList<ProductDimensionsModel>>(_repository.GetQuery().Where(d => d.ProductId == productId));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ProductDimensions> dimensions = _repository.GetQuery();

            dimensions = ApplyFilter(dimensions, request);

            return dimensions.Count();
        }

        public IList<ProductDimensionsModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ProductDimensions> dimensions = _repository.GetQuery();

            dimensions = ApplyFilter(dimensions, request);
            dimensions = ApplySorting(dimensions, request);
            dimensions = ApplyPaging(dimensions, request);

            return _mapper.Map<IList<ProductDimensionsModel>>(dimensions);
        }

        public void Add(ProductDimensionsModel dimensions)
        {
            ProductDimensions dimensionsToAdd = new();

            TransferValues(dimensionsToAdd, dimensions);

            _repository.Add(dimensionsToAdd);
            _unitOfWork.Save();
        }

        public void Update(ProductDimensionsModel dimensions)
        {
            ProductDimensions dimensionsToUpdate = _repository.Get(dimensions.Id);

            TransferValues(dimensionsToUpdate, dimensions);

            _repository.Update(dimensionsToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(ProductDimensions toDimensions, ProductDimensionsModel fromDimensions)
        {
            toDimensions.ProductId = fromDimensions.Product.Id;
            toDimensions.SizeId = fromDimensions.Size.Id;
            toDimensions.WeightInGrams = fromDimensions.WeightInGrams;
            toDimensions.WeightInPounds = fromDimensions.WeightInPounds;
            toDimensions.DepthInCentimeters = fromDimensions.DepthInCentimeters;
            toDimensions.DepthInInches = fromDimensions.DepthInInches;
            toDimensions.LengthInCentimeters = fromDimensions.LengthInCentimeters;
            toDimensions.LengthInInches = fromDimensions.LengthInInches;
            toDimensions.WidthInCentimeters = fromDimensions.WidthInCentimeters;
            toDimensions.WidthInInches = fromDimensions.WidthInInches;
            toDimensions.HeightInCentimeters = fromDimensions.HeightInCentimeters;
            toDimensions.HeightInInches = fromDimensions.HeightInInches;
        }

        private IQueryable<ProductDimensions> ApplyFilter(IQueryable<ProductDimensions> dimensions, ListRequest request)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (currentBrand != null && _userService.IsSpecificBrand())
            {
                dimensions = dimensions.Where(d => d.Product.BrandId == currentBrand.Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return dimensions;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "product", "brand", "size", "product_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    dimensions = searchField.Name switch
                    {
                        "id" => dimensions.Where(d => d.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => dimensions.Where(d => d.Product.Code.Contains(searchField.Value)),
                        "brand" => dimensions.Where(d => d.Product.Brand.Name.Contains(searchField.Value)),
                        "size" => dimensions.Where(d => d.Size.Name.Contains(searchField.Value)),
                        "product_id" => dimensions.Where(d => d.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    dimensions = dimensions.Where(d => d.Id == searchString.ParseToIntOrDefault()
                      || d.Product.Name.Contains(searchString)
                      || d.Product.Code.Contains(searchString)
                      || d.Product.Brand.Name.Contains(searchString)
                      || d.Size.Name.Contains(searchString));
                }
            }

            return dimensions;
        }

        private IQueryable<ProductDimensions> ApplySorting(IQueryable<ProductDimensions> dimensions, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                dimensions = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.Id) : dimensions.OrderByDescending(d => d.Id),
                    "product" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.Product.Code) : dimensions.OrderByDescending(d => d.Product.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.Product.Brand.Name) : dimensions.OrderByDescending(d => d.Product.Brand.Name),
                    "size" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.Size.Name) : dimensions.OrderByDescending(d => d.Size.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                dimensions = dimensions.OrderBy(d => d.Id);
            }

            return dimensions;
        }

        private IQueryable<ProductDimensions> ApplyPaging(IQueryable<ProductDimensions> dimensions, ListRequest request)
        {
            return request.ShowAll ? dimensions : dimensions.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
