using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces;
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

        public PhotoService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPhotoCloudService photoCloudService)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Photo>();
            _mapper = mapper;
            _photoCloudService = photoCloudService;
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

        public IList<PhotoModel> GetFromCloud()
        {
            IList<PhotoModel> photos = _photoCloudService.GetPhotosToUploadFromCloud();

            foreach (var photo in photos)
            {

            }

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
                string[] searchFieldNames = new[] { "id", "product", "variant", "instance", "type", "url" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    photos = searchField.Name switch
                    {
                        "id" => photos.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => photos.Where(c => c.Variant.Product.Name.Contains(searchField.Value)),
                        "variant" => photos.Where(c => c.Variant.Sku.Contains(searchField.Value)),
                        "instance" => photos.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        "type" => photos.Where(c => c.Type.Name.Contains(searchField.Value)),
                        "url" => photos.Where(c => c.Url.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    photos = photos.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Variant.Product.Name.Contains(searchString)
                        || c.Variant.Sku.Contains(searchString)
                        || c.Instance.Name.Contains(searchString)
                        || c.Type.Name.Contains(searchString)
                        || c.Url.Contains(searchString));
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
                    "id" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(c => c.Id) : photos.OrderByDescending(c => c.Id),
                    "product" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(c => c.Variant.Product.Name) : photos.OrderByDescending(c => c.Variant.Product.Name),
                    "variant" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(c => c.Variant.Sku) : photos.OrderByDescending(c => c.Variant.Sku),
                    "instance" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(c => c.Instance.Name) : photos.OrderByDescending(c => c.Instance.Name),
                    "type" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(c => c.Type.SystemName) : photos.OrderByDescending(c => c.Type.SystemName),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                photos = photos.OrderBy(c => c.Id);
            }

            return photos;
        }

        private IQueryable<Photo> ApplyPaging(IQueryable<Photo> photos, ListRequest request)
        {
            return photos.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
