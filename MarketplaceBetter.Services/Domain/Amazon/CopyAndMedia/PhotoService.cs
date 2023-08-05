using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia
{
    public class PhotoService : IPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Photo> _repository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IPhotoCloudService _photoCloudService;
        private readonly IUserService _userService;

        public PhotoService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPhotoCloudService photoCloudService,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Photo>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _photoCloudService = photoCloudService;
            _userService = userService;
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

        public void Add(PhotoModel photo)
        {
            Photo photoToAdd = new();

            TransferValues(photoToAdd, photo);

            photoToAdd.Uploaded = DateTime.Now;

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

        public void AddOrUpdate(PhotoModel photo, VariantModel variant)
        {
            if (_repository.Any(p => p.InstanceId == photo.Instance.Id && p.TypeId == photo.Type.Id && p.VariantId == variant.Id))
            {
                Photo photoToUpdate = _repository.Single(p => p.InstanceId == photo.Instance.Id && p.TypeId == photo.Type.Id && p.VariantId == variant.Id);
                TransferValues(photoToUpdate, photo, variant);

                _repository.Update(photoToUpdate);
            }
            else
            {
                Photo photoToAdd = new Photo();
                TransferValues(photoToAdd, photo, variant);

                _repository.Add(photoToAdd);
            }

            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            Photo photoToDelete = _repository.Get(id);

            _photoCloudService.Delete(photoToDelete.CloudId);

            _repository.Delete(photoToDelete);
            _unitOfWork.Save();
        }

        public void Delete(IList<PhotoModel> photos)
        {
            _photoCloudService.Delete(photos.Select(p => p.CloudId).ToList());

            foreach (var photo in photos)
            {
                Photo photoToDelete = _repository.Get(photo.Id);
                _repository.Delete(photoToDelete);
            }

            _unitOfWork.Save();
        }

        public void DeleteAllForVariant(long variantId)
        {
            IList<PhotoModel> photos = _mapper.Map<IList<PhotoModel>>(_repository.Where(p => p.VariantId == variantId).ToList());

            Delete(photos);
        }

        public void UpdateType(IList<PhotoModel> photos, PhotoTypeModel type)
        {
            foreach (var photo in photos)
            {
                Photo photoToUpdate = _repository.Get(photo.Id);

                photoToUpdate.TypeId = type.Id;

                _repository.Update(photoToUpdate);
                _unitOfWork.Save();
            }
        }

        private void TransferValues(Photo toPhoto, PhotoModel fromPhoto)
        {
            toPhoto.VariantId = fromPhoto.Variant.Id;
            toPhoto.InstanceId = fromPhoto.Instance.Id;
            toPhoto.TypeId = fromPhoto.Type.Id;
            toPhoto.CloudId = fromPhoto.CloudId;
            toPhoto.Version = fromPhoto.Version;
            toPhoto.Url = fromPhoto.Url;
            toPhoto.FileName = fromPhoto.FileName;
            toPhoto.Height = fromPhoto.Height;
            toPhoto.Width = fromPhoto.Width;
        }

        private void TransferValues(Photo toPhoto, PhotoModel fromPhoto, VariantModel variant)
        {
            toPhoto.VariantId = variant.Id;
            toPhoto.InstanceId = fromPhoto.Instance.Id;
            toPhoto.TypeId = fromPhoto.Type.Id;
            toPhoto.KindId = fromPhoto.Kind.Id;
            toPhoto.CloudId = fromPhoto.CloudId;
            toPhoto.Version = fromPhoto.Version;
            toPhoto.Url = fromPhoto.Url;
            toPhoto.FileName = fromPhoto.FileName;
            toPhoto.Height = fromPhoto.Height;
            toPhoto.Width = fromPhoto.Width;
            toPhoto.Uploaded = DateTime.Now;
        }

        private IQueryable<Photo> ApplyFilter(IQueryable<Photo> photos, ListRequest request)
        {
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                photos = photos.Where(p => p.Variant.Product.BrandId == currentBrand.Id);
            }

            if (_userService.IsSpecificCollection())
            {
                CollectionModel currentCollection = _userService.GetCurrentCollection();
                photos = photos.Where(p => p.Variant.Product.CollectionId == currentCollection.Id);
            }

            if (_userService.IsSpecificSize())
            {
                StandardSizeModel currentSize = _userService.GetCurrentSize();
                photos = photos.Where(p => p.Variant.Size.StandardSizeId == currentSize.Id);
            }

            if (_userService.IsSpecificInstance())
            {
                InstanceModel currentInstance = _userService.GetCurrentInstance();
                long instanceAllId = _instanceRepository.Single(i => i.SystemName == InstanceEnum.All).Id;
                photos = photos.Where(p => p.InstanceId == _userService.GetCurrentInstance().Id || p.InstanceId == instanceAllId);
            }

            if (_userService.IsSpecificStatus())
            {
                EntityStatusModel currentStatus = _userService.GetCurrentStatus();
                photos = photos.Where(p => p.Variant.StatusId == currentStatus.Id);
            }

            bool hideDrafts = _userService.HideDrafts();
            if (hideDrafts && !_userService.IsSpecificStatus())
            {
                photos = photos.Where(p => p.Variant.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool hideWithdrawn = _userService.HideWithdrawn();
            if (hideWithdrawn && !_userService.IsSpecificStatus())
            {
                photos = photos.Where(p => p.Variant.Status.SystemName != EntityStatusEnum.Withdrawn);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return photos;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "product", "product_id", "variant", "size", "color", "instance", "file_name", "type", "kind", "height", "width", "comment" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    photos = searchField.Name switch
                    {
                        "id" => photos.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => photos.Where(p => p.Variant.Product.Name.Contains(searchField.Value)),
                        "product_id" => photos.Where(p => p.Variant.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        "variant" => photos.Where(p => p.Variant.Sku.Contains(searchField.Value)),
                        "size" => photos.Where(p => p.Variant.Size.Name.Contains(searchField.Value)),
                        "color" => photos.Where(p => p.Variant.Color.Name.Contains(searchField.Value)),
                        "instance" => photos.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "file_name" => photos.Where(p => p.FileName.Contains(searchField.Value)),
                        "type" => photos.Where(p => p.Type.Name.Contains(searchField.Value)),
                        "kind" => photos.Where(p => p.Kind.Name.Contains(searchField.Value)),
                        "height" => photos.Where(p => p.Height == searchField.Value.ParseToIntOrDefault()),
                        "width" => photos.Where(p => p.Width == searchField.Value.ParseToIntOrDefault()),
                        "comment" => photos.Where(p => p.Comment.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    photos = photos.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Variant.Product.Name.Contains(searchString)
                        || p.Variant.Product.Id == searchString.ParseToIntOrDefault()
                        || p.Variant.Sku.Contains(searchString)
                        || p.Variant.Size.Name.Contains(searchString)
                        || p.Variant.Color.Name.Contains(searchString)
                        || p.Instance.Name.Contains(searchString)
                        || p.FileName.Contains(searchString)
                        || p.Type.Name.Contains(searchString)
                        || p.Kind.Name.Contains(searchString)
                        || p.Height == searchString.ParseToIntOrDefault()
                        || p.Width == searchString.ParseToIntOrDefault()
                        || p.Comment.Contains(searchString));
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
                    "id" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Id).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Id).ThenBy(p => p.Type.Id),
                    "product" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Variant.Product.Name).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Variant.Product.Name).ThenBy(p => p.Type.Id),
                    "variant" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Variant.Sku).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Variant.Sku).ThenBy(p => p.Type.Id),
                    "type" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Type.Name).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Type.Name).ThenBy(p => p.Type.Id),
                    "kind" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Kind.Name).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Kind.Name).ThenBy(p => p.Type.Id),
                    "instance" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Instance.Name).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Instance.Name).ThenBy(p => p.Type.Id),
                    "filename" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.FileName).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.FileName).ThenBy(p => p.Type.Id),
                    "height" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Height).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Height).ThenBy(p => p.Type.Id),
                    "width" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Width).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Width).ThenBy(p => p.Type.Id),
                    "uploaded" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Uploaded).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Uploaded).ThenBy(p => p.Type.Id),
                    "comment" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Comment).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Comment).ThenBy(p => p.Type.Id),
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
    }
}
