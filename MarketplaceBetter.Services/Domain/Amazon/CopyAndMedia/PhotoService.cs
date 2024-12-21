using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
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
using Microsoft.AspNetCore.Hosting;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

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
        private readonly string _downloadFolderPath;

        public PhotoService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPhotoCloudService photoCloudService,
            IUserService userService,
            IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Photo>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _photoCloudService = photoCloudService;
            _userService = userService;
            _downloadFolderPath = Path.Combine(environment.WebRootPath, "_download");
        }

        public PhotoModel Get(long id) => _mapper.Map<PhotoModel>(_repository.Get(id));

        public PhotoModel GetForVariant(long variantId, PhotoTypeEnum type, InstanceEnum instance) => _mapper.Map<PhotoModel>(_repository.SingleOrDefault(p => p.VariantId == variantId & p.Type.SystemName == type & p.Instance.SystemName == instance));

        public IList<PhotoModel> GetForVariantAndInstanceAll(long variantId) => _mapper.Map<IList<PhotoModel>>(_repository.Where(p => p.VariantId == variantId && p.Instance.SystemName == InstanceEnum.All));

        public int CountForListRequest(ListRequest request, bool showSharedOnly)
        {
            IQueryable<Photo> photos = _repository.GetQuery();

            photos = ApplyFilter(photos, request, showSharedOnly);

            return photos.Count();
        }

        public IList<PhotoModel> GetForListRequest(ListRequest request, bool showSharedOnly)
        {
            IQueryable<Photo> photos = _repository.GetQuery();

            photos = ApplyFilter(photos, request, showSharedOnly);
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

        public void UpdateType(PhotoModel photo, PhotoTypeModel type)
        {
            Photo photoToUpdate = _repository.Get(photo.Id);
            Variant variant = photoToUpdate.Variant;

            string fileName = $"{photoToUpdate.Instance.Name.ToLower()}_variant_{variant.Id}_{type.SystemName}";
            string fullFileName = $"product_{variant.Product.Id}/variant_{variant.Id}/instance_{photoToUpdate.Instance.Name.ToLower()}/{fileName}";

            if (photo.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            PhotoRenamingResult renamingResult = _photoCloudService.Rename(photoToUpdate.CloudId, fullFileName);

            photoToUpdate.TypeId = type.Id;
            photoToUpdate.FileName = fileName;
            photoToUpdate.CloudId = renamingResult.CloudId;
            photoToUpdate.Version = renamingResult.Version;
            photoToUpdate.Url = renamingResult.Url;

            _repository.Update(photoToUpdate);
            _unitOfWork.Save();
        }

        public async Task PrepareForDownload(PhotoModel photo, VariantModel variant)
        {
            if (variant.Asin == null)
            {
                return;
            }

            string url = _photoCloudService.GetOriginalUrl(photo.CloudId, photo.Version);
            string format = _photoCloudService.GetFormat(photo.CloudId);

            string fileName = $"{variant.Asin}.{photo.Type.AmazonUploadCode}.{format}";

            using HttpClient client = new HttpClient();
            using Stream stream = await client.GetStreamAsync(url);

            string path = Path.Combine(_downloadFolderPath, fileName);
            using FileStream file = new FileStream(path, FileMode.Create);

            stream.CopyTo(file);
        }

        public Stream DownloadPhotos()
        {
            MemoryStream memoryStream = new MemoryStream();

            using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in Directory.GetFiles(_downloadFolderPath))
                {
                    if (file.Contains("_placeholder", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    archive.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.NoCompression);
                }
            }

            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        public void ClearPhotosToDownload()
        {
            foreach (var file in Directory.GetFiles(_downloadFolderPath))
            {
                if (file.Contains("_placeholder", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                File.Delete(file);
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

        private IQueryable<Photo> ApplyFilter(IQueryable<Photo> photos, ListRequest request, bool showSharedOnly)
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
                if (currentSize.SystemName == StandardSizeEnum.OneSizePlusM)
                {
                    photos = photos.Where(p => p.Variant.Size.StandardSize.SystemName == StandardSizeEnum.OneSize || p.Variant.Size.StandardSize.SystemName == StandardSizeEnum.M);
                }
                else
                {
                    photos = photos.Where(p => p.Variant.Size.StandardSizeId == currentSize.Id);
                }
            }

            if (_userService.IsSpecificInstance())
            {
                InstanceModel currentInstance = _userService.GetCurrentInstance();
                long instanceAllId = _instanceRepository.Single(i => i.SystemName == InstanceEnum.All).Id;
                photos = photos.Where(p => p.InstanceId == _userService.GetCurrentInstance().Id || p.InstanceId == instanceAllId);
            }

            if (showSharedOnly)
            {
                long instanceAllId = _instanceRepository.Single(i => i.SystemName == InstanceEnum.All).Id;
                photos = photos.Where(p => p.InstanceId == instanceAllId);
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
                string[] searchFieldNames = new[] { "id", "product", "product_id", "variant", "variant_id", "asin", "size", "color", "instance", "file_name", "type", "kind", "height", "width", "comment" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    photos = searchField.Name switch
                    {
                        "id" => photos.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => photos.Where(p => p.Variant.Product.Name.Contains(searchField.Value)),
                        "product_id" => photos.Where(p => p.Variant.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        "variant" => photos.Where(p => p.Variant.Sku.Contains(searchField.Value)),
                        "variant_id" => photos.Where(p => p.VariantId == searchField.Value.ParseToIntOrDefault()),
                        "asin" => photos.Where(p => p.Variant.Asin.Contains(searchField.Value)),
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
                        || p.Variant.Asin.Contains(searchString)
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
                    "asin" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Variant.Asin).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Variant.Asin).ThenBy(p => p.Type.Id),
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
