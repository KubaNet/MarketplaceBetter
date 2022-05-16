using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Infrastructure.Helpers;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
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
        private readonly IRepository<ParentInstance> _repository;
        private readonly IRepository<Parent> _parentRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<ChildInstance> _childInstanceRepository;
        private readonly IRepository<ColorTranslation> _colorTranslationRepository;
        private readonly IRepository<Copywriting> _copywritingRepository;

        public ParentInstanceService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ParentInstance>();
            _parentRepository = unitOfWork.GetRepository<Parent>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _childInstanceRepository = unitOfWork.GetRepository<ChildInstance>();
            _colorTranslationRepository = unitOfWork.GetRepository<ColorTranslation>();
            _copywritingRepository = unitOfWork.GetRepository<Copywriting>();
        }

        public ParentInstanceModel Get(long id) => _mapper.Map<ParentInstanceModel>(_repository.Get(id));

        public IList<ParentInstanceModel> GetAll() => _mapper.Map<IList<ParentInstanceModel>>(_repository.GetQuery().OrderBy(p => p.Sku));

        public IList<ParentInstanceModel> GetAllForBrand(long brandId, long instanceId) => _mapper.Map<IList<ParentInstanceModel>>(
                _repository.GetQuery().Where(p => p.Parent.Product.BrandId == brandId).OrderBy(p => p.Sku));

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

            _repository.Add(parentInstanceToAdd);
            _unitOfWork.Save();
        }

        public void AddForParent(long parentId)
        {
            foreach (var instance in _instanceRepository.GetAll().OrderBy(i => i.Id))
            {
                if (_repository.Any(p => p.ParentId == parentId && p.InstanceId == instance.Id))
                {
                    continue;
                }

                ParentInstance parentInstance = new ParentInstance { 
                    ParentId = parentId, InstanceId = instance.Id, Sku = GetSkuFor(parentId, instance.Id) };

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

        public string GetSkuFor(long? parentId, long? instanceId)
        {
            if (parentId.HasValue && instanceId.HasValue)
            {
                Parent parent = _parentRepository.Get(parentId.Value);
                Instance instance = _instanceRepository.Get(instanceId.Value);

                return $"{AmazonInstanceHelper.GetCodeFor(instance.SystemName)}_{parent.Sku}";
            }
            else if (parentId.HasValue)
            {
                Parent parent = _parentRepository.Get(parentId.Value);

                return parent.Sku;
            }
            else if (instanceId.HasValue)
            {
                Instance instance = _instanceRepository.Get(instanceId.Value);

                return $"{AmazonInstanceHelper.GetCodeFor(instance.SystemName)}_";
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
                c.Child.ParentId == parentInstance.ParentId && c.InstanceId == instance.Id 
                && c.Child.Variant.Status.SystemName != VariantStatusEnum.Withdrawn).OrderBy(c => c.Sku).ToList();

            csv.WriteField("Seller SKU");
            csv.WriteField("Brand Name");
            csv.WriteField("Product Name");
            csv.WriteField("Product ID");
            csv.WriteField("Color Name");
            csv.WriteField("Color Map");
            csv.WriteField("Size Name");
            csv.NextRecord();

            foreach (var childInstance in childInstances)
            {
                ColorTranslation colorTranslation = _colorTranslationRepository.SingleOrDefault(t =>
                    t.ColorId == childInstance.Child.Variant.Color.Id && t.InstanceId == instance.Id);

                csv.WriteField(childInstance.Sku);
                csv.WriteField(childInstance.Child.Parent.Product.Brand.Name);
                csv.WriteField(GetProductName(childInstance, colorTranslation));
                csv.WriteField(childInstance.Child.Asin);
                csv.WriteField(colorTranslation?.Translation);
                csv.WriteField(colorTranslation?.Mapping);
                csv.WriteField(childInstance.Child.Variant.Size.Name);
                csv.NextRecord();
            }

            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private string GetProductName(ChildInstance childInstance, ColorTranslation colorTranslation)
        {
            Variant variant = childInstance.Child.Variant;
            Size size = variant.Size;
            Copywriting title = _copywritingRepository.SingleOrDefault(c => c.ProductId == variant.ProductId 
                && c.Element.SystemName == CopywritingElementEnum.Title);

            if (size.IsOneSize)
            {
                return $"{title} ({colorTranslation?.Translation})";
            }
            else
            {
                return $"{title} ({size.Code}, {colorTranslation?.Translation})";
            }
        }

        private void TransferValues(ParentInstance toParentInstance, ParentInstanceModel fromParentInstance)
        {
            toParentInstance.ParentId = fromParentInstance.Parent.Id;
            toParentInstance.InstanceId = fromParentInstance.Instance.Id;
            toParentInstance.Sku = fromParentInstance.Sku;
            toParentInstance.Asin = fromParentInstance.Asin;
        }

        private IQueryable<ParentInstance> ApplyFilter(IQueryable<ParentInstance> parents, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return parents;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "asin", "instance", "parent", "product", "brand", "product_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    parents = searchField.Name switch
                    {
                        "id" => parents.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => parents.Where(p => p.Sku.Contains(searchField.Value)),
                        "asin" => parents.Where(p => p.Asin.Contains(searchField.Value)),
                        "instance" => parents.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "parent" => parents.Where(p => p.Parent.Sku.Contains(searchField.Value)),
                        "product" => parents.Where(p => p.Parent.Product.Name.Contains(searchField.Value)),
                        "brand" => parents.Where(p => p.Parent.Product.Brand.Name.Contains(searchField.Value)),
                        "product_id" => parents.Where(p => p.Parent.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    parents = parents.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Sku.Contains(searchString)
                        || p.Asin.Contains(searchString)
                        || p.Instance.Name.Contains(searchString)
                        || p.Parent.Sku.Contains(searchString)
                        || p.Parent.Product.Name.Contains(searchString)
                        || p.Parent.Product.Brand.Name.Contains(searchString));
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
                    "parent" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Parent.Sku) : parents.OrderByDescending(p => p.Parent.Sku),
                    "product" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Parent.Product.Name) : parents.OrderByDescending(p => p.Parent.Product.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Parent.Product.Brand.Name) : parents.OrderByDescending(p => p.Parent.Product.Brand.Name),
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
