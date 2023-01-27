using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using MarketplaceBetter.Services.Domain.Sales.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MarketplaceBetter.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class PhotoUploadService : IPhotoUploadService
    {
        private readonly IPhotoCloudService _photoCloudService;
        private readonly IInstanceService _instanceService;
        private readonly IPhotoTypeService _photoTypeService;
        private readonly IBrandService _brandService;

        public PhotoUploadService(IPhotoCloudService photoCloudService,
            IInstanceService instanceService,
            IPhotoTypeService photoTypeService,
            IBrandService brandService)
        {
            _photoCloudService = photoCloudService;
            _instanceService = instanceService;
            _photoTypeService = photoTypeService;
            _brandService = brandService;
        }

        public IList<PhotoUploadModel> GetForListRequest(ListRequest request)
        {
            IList<PhotoUploadModel> photos = _photoCloudService.GetPhotosToUpload();

            foreach (var photo in photos)
            {
                photo.Variants = GetVariants(photo.FileName);
                photo.Instance = GetIntance(photo.FileName);
                photo.Type = GetPhotoType(photo.FileName);
            }

            photos = ApplyFilter(photos, request);
            photos = ApplySorting(photos, request);

            return photos;
        }

        public void Remove(string cloudId)
        {
            _photoCloudService.Delete(cloudId);
        }

        private IList<VariantModel> GetVariants(string fileName)
        {
            BrandModel brand = GetBrand(fileName);

            return new List<VariantModel>();
        }

        private InstanceModel GetIntance(string fileName)
        {
            IList<InstanceModel> instances = _instanceService.GetAll();

            foreach (var instance in instances)
            {
                if (fileName.StartsWith($"{instance.ShortName}_", StringComparison.OrdinalIgnoreCase))
                {
                    return instance;
                }
            }

            return instances.Single(i => i.SystemName == InstanceEnum.All);
        }

        private PhotoTypeModel GetPhotoType(string fileName)
        {
            string amazonUploadCode;

            if (fileName.Contains('.'))
            {
                amazonUploadCode = fileName.Substring(fileName.LastIndexOf(".") + 1, fileName.Length - fileName.LastIndexOf(".") - 1);
            }
            else
            {
                return null;
            }

            IList<PhotoTypeModel> photoTypes = _photoTypeService.GetAll();

            foreach (var photoType in photoTypes)
            {
                if (photoType.AmazonUploadCode.Equals(amazonUploadCode, StringComparison.OrdinalIgnoreCase))
                {
                    return photoType;
                }
            }

            return null;
        }

        private BrandModel GetBrand(string fileName)
        {
            if (StartsWithInstance(fileName))
            {
                fileName = fileName.Remove(0, 3);
            }

            return null;
        }

        private bool StartsWithInstance(string fileName)
        {
            IList<InstanceModel> instances = _instanceService.GetAll();

            return instances.Any(i => fileName.StartsWith($"{i.ShortName}_", StringComparison.OrdinalIgnoreCase));
        }

        private IList<PhotoUploadModel> ApplyFilter(IList<PhotoUploadModel> photos, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return photos;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "product", "variant", "instance", "type", "filename", "height", "width" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    photos = searchField.Name switch
                    {
                        "product" => photos.Where(p => p.Variants.Any(v => v.Product.Name.Contains(searchField.Value, StringComparison.OrdinalIgnoreCase))).ToList(),
                        "variant" => photos.Where(p => p.Variants.Any(v => v.Sku.Contains(searchField.Value, StringComparison.OrdinalIgnoreCase))).ToList(),
                        "instance" => photos.Where(p => p.Instance.Name.Contains(searchField.Value, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "type" => photos.Where(p => p.Type.Name.Contains(searchField.Value, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "filename" => photos.Where(p => p.FileName.Contains(searchField.Value, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "height" => photos.Where(p => p.Height == searchField.Value.ParseToIntOrDefault()).ToList(),
                        "width" => photos.Where(p => p.Width == searchField.Value.ParseToIntOrDefault()).ToList(),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    photos = photos.Where(p => p.Variants.Any(v => v.Product.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        || p.Variants.Any(v => v.Sku.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        || p.Instance.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        || (p.Type != null && p.Type.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        || p.FileName.Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                        || p.Height == searchString.ParseToIntOrDefault()
                        || p.Width == searchString.ParseToIntOrDefault()).ToList();
                }
            }

            return photos;
        }

        private IList<PhotoUploadModel> ApplySorting(IList<PhotoUploadModel> photos, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                photos = request.SortBy switch
                {
                    "instance" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Instance?.Name).ToList() : photos.OrderByDescending(p => p.Instance?.Name).ToList(),
                    "type" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Type?.SystemName).ToList() : photos.OrderByDescending(p => p.Type?.SystemName).ToList(),
                    "filename" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.FileName).ToList() : photos.OrderByDescending(p => p.FileName).ToList(),
                    "height" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Height).ToList() : photos.OrderByDescending(p => p.Height).ToList(),
                    "width" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Width).ToList() : photos.OrderByDescending(p => p.Width).ToList(),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                photos = photos.OrderBy(p => p.FileName).ToList();
            }

            return photos;
        }
    }
}
