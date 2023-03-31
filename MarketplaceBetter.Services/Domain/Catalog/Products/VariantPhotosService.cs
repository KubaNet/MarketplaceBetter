using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO.Compression;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MarketplaceBetter.Services.Specialized;

namespace MarketplaceBetter.Services.Domain.Catalog.Products
{
    public class VariantPhotosService : IVariantPhotosService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Photo> _repository;
        private readonly IRepository<Child> _childRepository;
        private readonly IPhotoCloudService _photoCloudService;
        private readonly ICurrentBrandService _currentBrandService;
        private readonly IWebHostEnvironment _environment;
        private readonly string _downloadFolderPath;

        public VariantPhotosService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPhotoCloudService photoCloudService,
            ICurrentBrandService currentBrandService,
            IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<Photo>();
            _childRepository = unitOfWork.GetRepository<Child>();
            _photoCloudService = photoCloudService;
            _currentBrandService = currentBrandService;
            _environment = environment;
            _downloadFolderPath = Path.Combine(_environment.WebRootPath, "_download");
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Photo> photos = _repository.GetQuery().Where(p => p.Variant.Status.SystemName == VariantStatusEnum.Active || p.Variant.Status.SystemName == VariantStatusEnum.ToAdd);

            photos = ApplyFilter(photos, request);

            var groupedPhotos = photos.GroupBy(p => new { p.VariantId, p.InstanceId });

            return groupedPhotos.Count();
        }

        public IList<VariantPhotosModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Photo> photos = _repository.GetQuery().Where(p => p.Variant.Status.SystemName == VariantStatusEnum.Active || p.Variant.Status.SystemName == VariantStatusEnum.ToAdd);

            photos = ApplyFilter(photos, request);
            photos = ApplySorting(photos, request);

            var groupedPhotos = photos.ToList().GroupBy(p => new { p.VariantId, p.InstanceId });

            if (!request.ShowAll)
            {
                groupedPhotos = groupedPhotos.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }

            return groupedPhotos.Select(p => new VariantPhotosModel
            {
                Variant = _mapper.Map<VariantModel>(p.First().Variant),
                Instance = _mapper.Map<InstanceModel>(p.First().Instance),
                Photos = _mapper.Map<IList<PhotoModel>>(p.ToList()),
            }).ToList();
        }

        public async void PrepareForDownload(PhotoModel photo, VariantModel variant)
        {
            Child child = _childRepository.SingleOrDefault(c => c.VariantId == variant.Id && c.Asin != null);
            if (child == null)
            {
                return;
            }

            string url = _photoCloudService.GetOriginalPhotoUrl(photo.CloudId, photo.Version);
            string format = _photoCloudService.GetPhotoFormat(photo.CloudId);

            string fileName = $"{child.Asin}.{photo.Type.AmazonUploadCode}.{format}";

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

                    archive.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
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

        private IQueryable<Photo> ApplyFilter(IQueryable<Photo> photos, ListRequest request)
        {
            if (_currentBrandService.IsSpecificBrand())
            {
                photos = photos.Where(p => p.Variant.Product.BrandId == _currentBrandService.GetCurrentBrand().Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return photos;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "variant", "instance", "product_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    photos = searchField.Name switch
                    {
                        "variant" => photos.Where(p => p.Variant.Sku.Contains(searchField.Value)),
                        "instance" => photos.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "product_id" => photos.Where(p => p.Variant.ProductId == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    photos = photos.Where(p => p.Variant.Sku.Contains(searchString)
                        || p.Instance.Name.Contains(searchString));
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
                    "variant" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Variant.Sku).ThenBy(p => p.InstanceId) : photos.OrderByDescending(p => p.Variant.Sku).ThenBy(p => p.InstanceId),
                    "instance" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Instance.Name).ThenBy(p => p.InstanceId) : photos.OrderByDescending(p => p.Instance.Name).ThenBy(p => p.InstanceId),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                photos = photos.OrderBy(p => p.Variant.Id).ThenBy(p => p.InstanceId);
            }

            return photos;
        }
    }
}
