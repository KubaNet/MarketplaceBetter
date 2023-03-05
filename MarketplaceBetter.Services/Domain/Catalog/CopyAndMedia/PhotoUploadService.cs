using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Infrastructure.Data;
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

namespace MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia
{
    public class PhotoUploadService : IPhotoUploadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PhotoUpload> _repository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoCloudService _photoCloudService;
        private readonly IPhotoTypeService _photoTypeService;

        public PhotoUploadService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPhotoCloudService photoCloudService,
            IPhotoTypeService photoTypeService)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<PhotoUpload>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _mapper = mapper;
            _photoCloudService = photoCloudService;
            _photoTypeService = photoTypeService;
        }

        public IList<PhotoUploadModel> GetForListRequest(ListRequest request)
        {
            IQueryable<PhotoUpload> photos = _repository.GetQuery();

            photos = ApplyFilter(photos, request);
            photos = ApplySorting(photos, request);

            return _mapper.Map<IList<PhotoUploadModel>>(photos);
        }

        public void AddOrUpdate(PhotoUploadModel photoUpload)
        {
            if (_repository.Any(p => p.CloudId == photoUpload.CloudId))
            {
                Update(photoUpload);
            }
            else
            {
                Add(photoUpload);
            }
        }

        public void Remove(PhotoUploadModel photoUpload)
        {
            _photoCloudService.Delete(photoUpload.CloudId);

            PhotoUpload photoUploadToDelete = _repository.Get(photoUpload.Id);
            _repository.Delete(photoUploadToDelete);
            _unitOfWork.Save();
        }

        public void Remove(IList<PhotoUploadModel> photoUploads)
        {
            foreach (var photoUpload in photoUploads)
            {
                Remove(photoUpload);
            }
        }

        public void SetType(IList<PhotoUploadModel> photoUploads, PhotoTypeModel type)
        {
            foreach (var photoUpload in photoUploads)
            {
                PhotoUpload photoUploadToUpdate = _repository.Get(photoUpload.Id);

                photoUploadToUpdate.TypeId = type.Id;

                _repository.Update(photoUploadToUpdate);
            }

            _unitOfWork.Save();
        }

        private void Add(PhotoUploadModel photoUpload)
        {
            PhotoUpload photoUploadToAdd = new();

            TransferValues(photoUploadToAdd, photoUpload);

            photoUploadToAdd.Instance = GetIntance(photoUpload.FileName);

            _repository.Add(photoUploadToAdd);
            _unitOfWork.Save();
        }

        private void Update(PhotoUploadModel photoUpload)
        {
            PhotoUpload photoUploadToAdd = _repository.Single(p => p.CloudId == photoUpload.CloudId);

            TransferValues(photoUploadToAdd, photoUpload);

            photoUploadToAdd.Instance = GetIntance(photoUpload.FileName);

            _repository.Update(photoUploadToAdd);
            _unitOfWork.Save();
        }

        private void TransferValues(PhotoUpload toPhotoUpload, PhotoUploadModel fromPhotoUpload)
        {
            toPhotoUpload.FileName = fromPhotoUpload.FileName;
            toPhotoUpload.CloudId = fromPhotoUpload.CloudId;
            toPhotoUpload.Version = fromPhotoUpload.Version;
            toPhotoUpload.Url = fromPhotoUpload.Url;
            toPhotoUpload.Height = fromPhotoUpload.Height;
            toPhotoUpload.Width = fromPhotoUpload.Width;
        }

        private IList<VariantModel> GetVariants(string fileName)
        {
            BrandModel brand = GetBrand(fileName);

            return new List<VariantModel>();
        }

        private Instance GetIntance(string fileName)
        {
            IList<Instance> instances = _instanceRepository.GetAll();

            foreach (var instance in instances)
            {
                if (fileName.StartsWith($"{instance.ShortName}_", StringComparison.OrdinalIgnoreCase))
                {
                    return instance;
                }
            }

            return null;
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
            IList<Instance> instances = _instanceRepository.GetAll();

            return instances.Any(i => fileName.StartsWith($"{i.ShortName}_", StringComparison.OrdinalIgnoreCase));
        }

        private IQueryable<PhotoUpload> ApplyFilter(IQueryable<PhotoUpload> photos, ListRequest request)
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
                        "product" => photos.Where(p => p.Variants.Any(v => v.Variant.Product.Name.Contains(searchField.Value))),
                        "variant" => photos.Where(p => p.Variants.Any(v => v.Variant.Sku.Contains(searchField.Value))),
                        "instance" => photos.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "type" => photos.Where(p => p.Type != null && p.Type.Name.Contains(searchField.Value)),
                        "filename" => photos.Where(p => p.FileName.Contains(searchField.Value)),
                        "height" => photos.Where(p => p.Height == searchField.Value.ParseToIntOrDefault()),
                        "width" => photos.Where(p => p.Width == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    photos = photos.Where(p => p.Variants.Any(v => v.Variant.Product.Name.Contains(searchString))
                        || p.Variants.Any(v => v.Variant.Sku.Contains(searchString))
                        || p.Instance.Name.Contains(searchString)
                        || p.Type != null && p.Type.Name.Contains(searchString)
                        || p.FileName.Contains(searchString)
                        || p.Height == searchString.ParseToIntOrDefault()
                        || p.Width == searchString.ParseToIntOrDefault());
                }
            }

            return photos;
        }

        private IQueryable<PhotoUpload> ApplySorting(IQueryable<PhotoUpload> photos, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                photos = request.SortBy switch
                {
                    "instance" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Instance.Name) : photos.OrderByDescending(p => p.Instance.Name),
                    "type" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Type.SystemName) : photos.OrderByDescending(p => p.Type.SystemName),
                    "filename" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.FileName) : photos.OrderByDescending(p => p.FileName),
                    "height" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Height) : photos.OrderByDescending(p => p.Height),
                    "width" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Width) : photos.OrderByDescending(p => p.Width),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                photos = photos.OrderBy(p => p.FileName);
            }

            return photos;
        }
    }
}
