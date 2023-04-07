using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Infrastructure.Helpers;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Size = MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes.Size;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class ParentInstanceService : IParentInstanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;
        private readonly ICopywritingService _copywritingService;
        private readonly IRepository<ParentInstance> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<EntityStatus> _statusRepository;
        private readonly IRepository<ChildInstance> _childInstanceRepository;
        private readonly IRepository<ColorTranslation> _colorTranslationRepository;
        private readonly ICurrentBrandService _currentBrandService;
        private readonly ICurrentInstanceService _currentInstanceService;

        public ParentInstanceService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPhotoService photoService,
            ICopywritingService copywritingService,
            ICurrentBrandService currentBrandService,
            ICurrentInstanceService currentInstanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _copywritingService = copywritingService;
            _repository = unitOfWork.GetRepository<ParentInstance>();
            _productRepository = unitOfWork.GetRepository<Product>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _childInstanceRepository = unitOfWork.GetRepository<ChildInstance>();
            _colorTranslationRepository = unitOfWork.GetRepository<ColorTranslation>();
            _currentBrandService = currentBrandService;
            _currentInstanceService = currentInstanceService;
        }

        public ParentInstanceModel Get(long id) => _mapper.Map<ParentInstanceModel>(_repository.Get(id));

        public IList<ParentInstanceModel> GetAll() => _mapper.Map<IList<ParentInstanceModel>>(_repository.GetQuery().OrderBy(p => p.Sku));

        public IList<ParentInstanceModel> GetAllForBrand(long brandId, long instanceId) => _mapper.Map<IList<ParentInstanceModel>>(
                _repository.GetQuery().Where(p => p.Product.BrandId == brandId).OrderBy(p => p.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ParentInstance> parentInstances = _repository.GetQuery();

            parentInstances = ApplyFilter(parentInstances, request);

            return parentInstances.Count();
        }

        public IList<ParentInstanceModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ParentInstance> parentInstances = _repository.GetQuery();

            parentInstances = ApplyFilter(parentInstances, request);
            parentInstances = ApplySorting(parentInstances, request);
            parentInstances = ApplyPaging(parentInstances, request);

            return _mapper.Map<IList<ParentInstanceModel>>(parentInstances);
        }

        public void Add(ParentInstanceModel parentInstance)
        {
            ParentInstance parentInstanceToAdd = new();

            TransferValues(parentInstanceToAdd, parentInstance);
            parentInstanceToAdd.Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft);

            _repository.Add(parentInstanceToAdd);
            _unitOfWork.Save();
        }

        public void AddForProduct(long productId)
        {
            foreach (var instance in _instanceRepository.Where(i => i.IsNormal).OrderBy(i => i.Id))
            {
                if (_repository.Any(p => p.ProductId == productId && p.InstanceId == instance.Id))
                {
                    continue;
                }

                ParentInstance parentInstance = new ParentInstance 
                { 
                    ProductId = productId, 
                    InstanceId = instance.Id, 
                    Sku = GetSkuFor(productId, instance.Id),
                    Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft)
                };

                _repository.Add(parentInstance);
                _unitOfWork.Save();
            }
        }

        public void Update(ParentInstanceModel parentInstance)
        {
            ParentInstance parentInstanceToUpdate = _repository.Get(parentInstance.Id);

            TransferValues(parentInstanceToUpdate, parentInstance);

            _repository.Update(parentInstanceToUpdate);
            _unitOfWork.Save();
        }

        public string GetSkuFor(long? productId, long? instanceId)
        {
            if (productId.HasValue && instanceId.HasValue)
            {
                Product product = _productRepository.Get(productId.Value);
                Instance instance = _instanceRepository.Get(instanceId.Value);

                return $"{InstanceHelper.GetCodeFor(instance.SystemName)}_{product.Brand.Code.ToLower()}_{product.Code}";
            }
            else if (productId.HasValue)
            {
                Product product = _productRepository.Get(productId.Value);

                return $"{product.Brand.Code.ToLower()}_{product.Code}"; ;
            }
            else if (instanceId.HasValue)
            {
                Instance instance = _instanceRepository.Get(instanceId.Value);

                return $"{InstanceHelper.GetCodeFor(instance.SystemName)}_";
            }

            return null;
        }

        public Stream Export(long id)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.UTF8,
                HasHeaderRecord = false,
            };

            CsvWriter csv = new CsvWriter(writer, config);

            ParentInstance parentInstance = _repository.Get(id);
            Instance instance = parentInstance.Instance;
            IList<ChildInstance> childInstances = _childInstanceRepository.Where(c => 
                c.Variant.ProductId == parentInstance.ProductId && c.InstanceId == instance.Id 
                && c.Variant.Status.SystemName != EntityStatusEnum.Withdrawn).OrderBy(c => c.Sku).ToList();

            csv.WriteField("Seller SKU");
            csv.WriteField("Brand Name");
            csv.WriteField("Product Name");
            csv.WriteField("Product ID");
            csv.WriteField("Color Name");
            csv.WriteField("Color Map");
            csv.WriteField("Size Name");
            // copywriting
            csv.WriteField("Description");
            csv.WriteField("Bullet Point 1");
            csv.WriteField("Bullet Point 2");
            csv.WriteField("Bullet Point 3");
            csv.WriteField("Bullet Point 4");
            csv.WriteField("Bullet Point 5");
            // images
            csv.WriteField("Main Image");
            csv.WriteField("Other Image 1");
            csv.WriteField("Other Image 2");
            csv.WriteField("Other Image 3");
            csv.WriteField("Other Image 4");
            csv.WriteField("Other Image 5");
            csv.WriteField("Other Image 6");
            csv.WriteField("Other Image 7");
            csv.WriteField("Other Image 8");
            csv.WriteField("Swatch Image");
            csv.NextRecord();

            foreach (var childInstance in childInstances)
            {
                ColorTranslation colorTranslation = _colorTranslationRepository.SingleOrDefault(t =>
                    t.ColorId == childInstance.Variant.Color.Id && t.InstanceId == instance.Id);

                csv.WriteField(childInstance.Sku);
                csv.WriteField(childInstance.Variant.Product.Brand.Name);
                csv.WriteField(GetProductName(childInstance, colorTranslation));
                csv.WriteField(childInstance.Variant.Asin);
                csv.WriteField(colorTranslation?.Translation);
                csv.WriteField(colorTranslation?.Mapping);
                csv.WriteField(childInstance.Variant.Size.Name);
                WriteCopywriting(csv, childInstance);
                WritePhotos(csv, childInstance);
                csv.NextRecord();
            }

            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private void WriteCopywriting(CsvWriter csv, ChildInstance childInstance)
        {
            CopywritingModel description = _copywritingService.GetForProduct(childInstance.Variant.ProductId, childInstance.InstanceId, CopywritingElementEnum.Description);
            csv.WriteField(description?.Value);
            CopywritingModel bulletPoint1 = _copywritingService.GetForProduct(childInstance.Variant.ProductId, childInstance.InstanceId, CopywritingElementEnum.BulletPoint1);
            csv.WriteField(bulletPoint1?.Value);
            CopywritingModel bulletPoint2 = _copywritingService.GetForProduct(childInstance.Variant.ProductId, childInstance.InstanceId, CopywritingElementEnum.BulletPoint2);
            csv.WriteField(bulletPoint2?.Value);
            CopywritingModel bulletPoint3 = _copywritingService.GetForProduct(childInstance.Variant.ProductId, childInstance.InstanceId, CopywritingElementEnum.BulletPoint3);
            csv.WriteField(bulletPoint3?.Value);
            CopywritingModel bulletPoint4 = _copywritingService.GetForProduct(childInstance.Variant.ProductId, childInstance.InstanceId, CopywritingElementEnum.BulletPoint4);
            csv.WriteField(bulletPoint4?.Value);
            CopywritingModel bulletPoint5 = _copywritingService.GetForProduct(childInstance.Variant.ProductId, childInstance.InstanceId, CopywritingElementEnum.BulletPoint5);
            csv.WriteField(bulletPoint5?.Value);
        }

        private void WritePhotos(CsvWriter csv, ChildInstance childInstance)
        {
            PhotoModel main = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Main);
            csv.WriteField(main?.Url);
            PhotoModel other1 = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Other1);
            csv.WriteField(other1?.Url);
            PhotoModel other2 = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Other2);
            csv.WriteField(other2?.Url);
            PhotoModel other3 = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Other3);
            csv.WriteField(other3?.Url);
            PhotoModel other4 = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Other4);
            csv.WriteField(other4?.Url);
            PhotoModel other5 = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Other5);
            csv.WriteField(other5?.Url);
            PhotoModel other6 = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Other6);
            csv.WriteField(other6?.Url);
            PhotoModel other7 = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Other7);
            csv.WriteField(other7?.Url);
            PhotoModel other8 = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Other8);
            csv.WriteField(other8?.Url);
            PhotoModel swatch = _photoService.GetForVariant(childInstance.Variant.Id, PhotoTypeEnum.Swatch);
            csv.WriteField(swatch?.Url);
        }

        private string GetProductName(ChildInstance childInstance, ColorTranslation colorTranslation)
        {
            Variant variant = childInstance.Variant;
            Size size = variant.Size;
            CopywritingModel title = _copywritingService.GetForProduct(variant.ProductId, childInstance.InstanceId, CopywritingElementEnum.Title);

            if (size.IsOneSize)
            {
                return $"{title.Value} ({colorTranslation?.Translation})";
            }
            else
            {
                return $"{title.Value} ({size.Code}, {colorTranslation?.Translation})";
            }
        }

        private void TransferValues(ParentInstance toParentInstance, ParentInstanceModel fromParentInstance)
        {
            toParentInstance.ProductId = fromParentInstance.Product.Id;
            toParentInstance.InstanceId = fromParentInstance.Instance.Id;
            toParentInstance.Sku = fromParentInstance.Sku;
            toParentInstance.Asin = fromParentInstance.Asin;
        }

        private IQueryable<ParentInstance> ApplyFilter(IQueryable<ParentInstance> parents, ListRequest request)
        {
            if (_currentBrandService.IsSpecificBrand())
            {
                parents = parents.Where(p => p.Product.BrandId == _currentBrandService.GetCurrentBrand().Id);
            }

            if (_currentInstanceService.IsSpecificInstance())
            {
                parents = parents.Where(p => p.InstanceId == _currentInstanceService.GetCurrentInstance().Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return parents;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "asin", "instance", "status", "product", "brand", "product_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    parents = searchField.Name switch
                    {
                        "id" => parents.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => parents.Where(p => p.Sku.Contains(searchField.Value)),
                        "asin" => parents.Where(p => p.Asin.Contains(searchField.Value)),
                        "instance" => parents.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "status" => parents.Where(p => p.Status.Name.Contains(searchField.Value)),
                        "product" => parents.Where(p => p.Product.Name.Contains(searchField.Value)),
                        "brand" => parents.Where(p => p.Product.Brand.Name.Contains(searchField.Value)),
                        "product_id" => parents.Where(p => p.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    parents = parents.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Sku.Contains(searchString)
                        || p.Asin.Contains(searchString)
                        || p.Instance.Name.Contains(searchString)
                        || p.Status.Name.Contains(searchString)
                        || p.Product.Name.Contains(searchString)
                        || p.Product.Brand.Name.Contains(searchString));
                }
            }

            return parents;
        }

        private IQueryable<ParentInstance> ApplySorting(IQueryable<ParentInstance> parents, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                parents = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Id) : parents.OrderByDescending(p => p.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Sku) : parents.OrderByDescending(p => p.Sku),
                    "asin" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Asin) : parents.OrderByDescending(p => p.Asin),
                    "instance" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Instance.Name) : parents.OrderByDescending(p => p.Instance.Name),
                    "status" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Status.Name) : parents.OrderByDescending(p => p.Status.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Product.Name) : parents.OrderByDescending(p => p.Product.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Product.Brand.Name) : parents.OrderByDescending(p => p.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return parents;
        }

        private IQueryable<ParentInstance> ApplyPaging(IQueryable<ParentInstance> parents, ListRequest request)
        {
            return request.ShowAll ? parents : parents.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
