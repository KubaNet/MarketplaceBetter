using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces;
using MarketplaceBetter.Services.Domain.Sales.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Photo> _repository;
        private readonly IMapper _mapper;
        private readonly IPhotoCloudService _photoCloudService;
        private readonly IInstanceService _instanceService;
        private readonly IPhotoTypeService _photoTypeService;

        public PhotoService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPhotoCloudService photoCloudService,
            IInstanceService instanceService,
            IPhotoTypeService photoTypeService)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Photo>();
            _mapper = mapper;
            _photoCloudService = photoCloudService;
            _instanceService = instanceService;
            _photoTypeService = photoTypeService;
        }

        public PhotoModel Get(long id) => _mapper.Map<PhotoModel>(_repository.Get(id));

        public PhotoModel GetForVariant(long variantId, PhotoTypeEnum type)
        {
            Photo photo = _repository.SingleOrDefault(p => p.VariantId == variantId & p.Type.SystemName == type);

            return _mapper.Map<PhotoModel>(photo);
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Photo> photos = _repository.GetQuery();

            photos = ApplyFilter(photos, request);

            return photos.Count();
        }

        public IList<PhotoModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Photo> photos = _repository.GetQuery();

            photos = ApplyFilter(photos, request);
            photos = ApplySorting(photos, request);
            photos = ApplyPaging(photos, request);

            return _mapper.Map<IList<PhotoModel>>(photos);
        }

        public IList<PhotoToUploadModel> GetAllToUpload(ListRequest request)
        {
            IList<PhotoToUploadModel> photos = _photoCloudService.GetPhotosToUpload();

            foreach (var photo in photos)
            {
                photo.Instance = GetIntanceFromFileName(photo.FileName);
                photo.Type = GetTypeFromFileName(photo.FileName);
            }

            photos = ApplySorting(photos, request);

            return photos;
        }

        public void Add(PhotoModel photo)
        {
            Photo photoToAdd = new();

            TransferValues(photoToAdd, photo);

            _repository.Add(photoToAdd);
            _unitOfWork.Save();
        }

        public void Update(PhotoModel photo)
        {
            Photo photoToUpdate = _repository.Get(photo.Id);

            TransferValues(photoToUpdate, photo);

            _repository.Update(photoToUpdate);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            Photo photoToDelete = _repository.Get(id);

            _photoCloudService.Delete(photoToDelete.CloudId);

            _repository.Delete(photoToDelete);
            _unitOfWork.Save();
        }

        public void DeleteFromCloud(string cloudId)
        {
            _photoCloudService.Delete(cloudId);
        }

        private InstanceModel GetIntanceFromFileName(string fileName)
        {
            IList<InstanceModel> instances = _instanceService.GetAll();

            return PhotoFromCloudHelper.GetIntance(instances, fileName);
        }

        private PhotoTypeModel GetTypeFromFileName(string fileName)
        {
            IList<PhotoTypeModel> types = _photoTypeService.GetAll();

            return PhotoFromCloudHelper.GetType(types, fileName);
        }

        private void TransferValues(Photo toPhoto, PhotoModel fromPhoto)
        {
            toPhoto.VariantId = fromPhoto.Variant.Id;
            toPhoto.InstanceId = fromPhoto.Instance?.Id;
            toPhoto.ForAllInstances = fromPhoto.Instance == null ? true : false;
            toPhoto.TypeId = fromPhoto.Type.Id;
            toPhoto.CloudId = fromPhoto.CloudId;
            toPhoto.Url = fromPhoto.Url;
        }

        private IQueryable<Photo> ApplyFilter(IQueryable<Photo> photos, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return photos;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "product", "variant", "instance", "type" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    photos = searchField.Name switch
                    {
                        "id" => photos.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => photos.Where(p => p.Variant.Product.Name.Contains(searchField.Value)),
                        "variant" => photos.Where(p => p.Variant.Sku.Contains(searchField.Value)),
                        "instance" => photos.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "type" => photos.Where(p => p.Type.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    photos = photos.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Variant.Product.Name.Contains(searchString)
                        || p.Variant.Sku.Contains(searchString)
                        || p.Instance.Name.Contains(searchString)
                        || p.Type.Name.Contains(searchString));
                }
            }

            return photos;
        }

        private IQueryable<Photo> ApplySorting(IQueryable<Photo> photos, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                photos = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Id) : photos.OrderByDescending(p => p.Id),
                    "product" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Variant.Product.Name) : photos.OrderByDescending(p => p.Variant.Product.Name),
                    "variant" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Variant.Sku) : photos.OrderByDescending(p => p.Variant.Sku),
                    "instance" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Instance.Name) : photos.OrderByDescending(p => p.Instance.Name),
                    "type" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Type.SystemName) : photos.OrderByDescending(p => p.Type.SystemName),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                photos = photos.OrderBy(p => p.Id);
            }

            return photos;
        }

        private IQueryable<Photo> ApplyPaging(IQueryable<Photo> photos, ListRequest request)
        {
            return photos.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }

        private IList<PhotoToUploadModel> ApplySorting(IList<PhotoToUploadModel> photos, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                photos = request.SortBy switch
                {
                    "instance" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Instance?.Name).ToList() : photos.OrderByDescending(p => p.Instance?.Name).ToList(),
                    "type" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Type?.SystemName).ToList() : photos.OrderByDescending(p => p.Type?.SystemName).ToList(),
                    "filename" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.FileName).ToList() : photos.OrderByDescending(p => p.FileName).ToList(),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return photos;
        }
    }
}
