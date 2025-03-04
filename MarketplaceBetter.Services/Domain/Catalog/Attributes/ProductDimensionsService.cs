using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Attributes;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Attributes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
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
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
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
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                dimensions = dimensions.Where(d => d.Product.BrandId == currentBrand.Id);
            }

            if (_userService.IsSpecificCollection())
            {
                CollectionModel currentCollection = _userService.GetCurrentCollection();
                dimensions = dimensions.Where(d => d.Product.CollectionId == currentCollection.Id);
            }

            if (_userService.IsSpecificSize())
            {
                StandardSizeModel currentSize = _userService.GetCurrentSize();
                if (currentSize.SystemName == StandardSizeEnum.OneSizePlusM)
                {
                    dimensions = dimensions.Where(d => d.Size.StandardSize.SystemName == StandardSizeEnum.OneSize || d.Size.StandardSize.SystemName == StandardSizeEnum.M);
                }
                else
                {
                    dimensions = dimensions.Where(d => d.Size.StandardSizeId == currentSize.Id);
                }
            }

            if (_userService.IsSpecificStatus())
            {
                EntityStatusModel currentStatus = _userService.GetCurrentStatus();
                dimensions = dimensions.Where(d => d.Product.StatusId == currentStatus.Id);
            }

            bool hideDrafts = _userService.HideDrafts();
            if (hideDrafts && !_userService.IsSpecificStatus())
            {
                dimensions = dimensions.Where(d => d.Product.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool hideWithdrawn = _userService.HideWithdrawn();
            if (hideWithdrawn && !_userService.IsSpecificStatus())
            {
                dimensions = dimensions.Where(d => d.Product.Status.SystemName != EntityStatusEnum.Withdrawn);
            }

            bool hideCopies = _userService.HideCopies();
            if (hideCopies)
            {
                dimensions = dimensions.Where(v => v.Product.IsSizeCopy == false);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return dimensions;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "product", "product_id", "brand", "size", "weight_g" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    dimensions = searchField.Name switch
                    {
                        "id" => dimensions.Where(d => d.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => dimensions.Where(d => d.Product.Code.Contains(searchField.Value)),
                        "product_id" => dimensions.Where(d => d.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        "size" => dimensions.Where(d => d.Size.Name.Contains(searchField.Value)),
                        "brand" => dimensions.Where(d => d.Product.Brand.Name.Contains(searchField.Value)),
                        "weight_g" => dimensions.Where(d => d.WeightInGrams == searchField.Value.ParseToDoubleOrDefault()),
                        "weight_lb" => dimensions.Where(d => d.WeightInPounds == searchField.Value.ParseToDoubleOrDefault()),
                        "depth_cm" => dimensions.Where(d => d.DepthInCentimeters == searchField.Value.ParseToDoubleOrDefault()),
                        "depth_in" => dimensions.Where(d => d.DepthInInches == searchField.Value.ParseToDoubleOrDefault()),
                        "length_cm" => dimensions.Where(d => d.LengthInCentimeters == searchField.Value.ParseToDoubleOrDefault()),
                        "length_in" => dimensions.Where(d => d.LengthInInches == searchField.Value.ParseToDoubleOrDefault()),
                        "width_cm" => dimensions.Where(d => d.WidthInCentimeters == searchField.Value.ParseToDoubleOrDefault()),
                        "width_in" => dimensions.Where(d => d.WidthInInches == searchField.Value.ParseToDoubleOrDefault()),
                        "height_cm" => dimensions.Where(d => d.HeightInCentimeters == searchField.Value.ParseToDoubleOrDefault()),
                        "height_in" => dimensions.Where(d => d.HeightInInches == searchField.Value.ParseToDoubleOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    dimensions = dimensions.Where(d => d.Id == searchString.ParseToIntOrDefault()
                      || d.Product.Name.Contains(searchString)
                      || d.Product.Code.Contains(searchString)
                      || d.Size.Name.Contains(searchString)
                      || d.Product.Brand.Name.Contains(searchString)
                      || d.WeightInGrams == searchString.ParseToDoubleOrDefault()
                      || d.WeightInPounds == searchString.ParseToDoubleOrDefault()
                      || d.DepthInCentimeters == searchString.ParseToDoubleOrDefault()
                      || d.DepthInInches == searchString.ParseToDoubleOrDefault()
                      || d.LengthInCentimeters == searchString.ParseToDoubleOrDefault()
                      || d.LengthInInches == searchString.ParseToDoubleOrDefault()
                      || d.WidthInCentimeters == searchString.ParseToDoubleOrDefault()
                      || d.WidthInInches == searchString.ParseToDoubleOrDefault()
                      || d.HeightInCentimeters == searchString.ParseToDoubleOrDefault()
                      || d.HeightInInches == searchString.ParseToDoubleOrDefault()
                      );
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
                    "size" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.Size.Name) : dimensions.OrderByDescending(d => d.Size.Name),
                    "weight_g" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.WeightInGrams) : dimensions.OrderByDescending(d => d.WeightInGrams),
                    "weight_lb" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.WeightInPounds) : dimensions.OrderByDescending(d => d.WeightInPounds),
                    "depth_cm" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.DepthInCentimeters) : dimensions.OrderByDescending(d => d.DepthInCentimeters),
                    "depth_in" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.DepthInInches) : dimensions.OrderByDescending(d => d.DepthInInches),
                    "length_cm" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.LengthInCentimeters) : dimensions.OrderByDescending(d => d.LengthInCentimeters),
                    "length_in" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.LengthInInches) : dimensions.OrderByDescending(d => d.LengthInInches),
                    "width_cm" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.WidthInCentimeters) : dimensions.OrderByDescending(d => d.WidthInCentimeters),
                    "width_in" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.WidthInInches) : dimensions.OrderByDescending(d => d.WidthInInches),
                    "height_cm" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.HeightInCentimeters) : dimensions.OrderByDescending(d => d.HeightInCentimeters),
                    "height_in" => request.SortDirection == SortDirection.Ascending ? dimensions.OrderBy(d => d.HeightInInches) : dimensions.OrderByDescending(d => d.HeightInInches),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                dimensions = dimensions.OrderBy(d => d.Product.Code).ThenBy(d => d.SizeId);
            }

            return dimensions;
        }

        private IQueryable<ProductDimensions> ApplyPaging(IQueryable<ProductDimensions> dimensions, ListRequest request)
        {
            return request.ShowAll ? dimensions : dimensions.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
