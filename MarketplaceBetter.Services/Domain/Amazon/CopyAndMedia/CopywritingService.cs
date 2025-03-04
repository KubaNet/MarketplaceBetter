using AutoMapper;
using CsvHelper.Configuration;
using CsvHelper;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
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
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using Microsoft.Extensions.Hosting;

namespace MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia
{
    public class CopywritingService : ICopywritingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Copywriting> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<CopywritingElement> _copywritingElementRepository;
        private readonly IUserService _userService;

        public CopywritingService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Copywriting>();
            _productRepository = unitOfWork.GetRepository<Product>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _copywritingElementRepository = unitOfWork.GetRepository<CopywritingElement>();
            _userService = userService;
        }

        public CopywritingModel Get(long id) => _mapper.Map<CopywritingModel>(_repository.Get(id));

        public CopywritingModel GetForProduct(long productId, long instanceId, CopywritingElementEnum element)
        {
            Copywriting copywriting = _repository.SingleOrDefault(c => c.ProductId == productId & c.InstanceId == instanceId & c.Element.SystemName == element);

            return _mapper.Map<CopywritingModel>(copywriting);
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Copywriting> copywritings = _repository.GetQuery();

            copywritings = ApplyFilter(copywritings, request);

            return copywritings.Count();
        }

        public IList<CopywritingModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Copywriting> copywritings = _repository.GetQuery();

            copywritings = ApplyFilter(copywritings, request);
            copywritings = ApplySorting(copywritings, request);
            copywritings = ApplyPaging(copywritings, request);

            return _mapper.Map<IList<CopywritingModel>>(copywritings);
        }

        public void Add(CopywritingModel copywriting)
        {
            Copywriting copywritingToAdd = new();

            TransferValues(copywritingToAdd, copywriting);

            _repository.Add(copywritingToAdd);
            _unitOfWork.Save();
        }

        public void Update(CopywritingModel copywriting)
        {
            Copywriting copywritingToUpdate = _repository.Get(copywriting.Id);

            TransferValues(copywritingToUpdate, copywriting);

            _repository.Update(copywritingToUpdate);
            _unitOfWork.Save();
        }

        public Stream GetMissing()
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

            csv.WriteField("Product");
            csv.WriteField("Instance");
            csv.WriteField("Copywriting Element");
            csv.NextRecord();

            IQueryable<Product> products = _productRepository.GetQuery();
            products = FilterProducts(products);

            foreach (var product in products.ToList().OrderBy(p => p.Order))
            {
                foreach (var instance in _instanceRepository.GetAll().Where(i => i.SystemName != InstanceEnum.All).OrderBy(i => i.SystemName))
                {
                    if (_userService.IsSpecificInstance() && _userService.GetCurrentInstance().SystemName != instance.SystemName)
                    {
                        continue;
                    }

                    foreach (var element in _copywritingElementRepository.GetAll().OrderBy(e => e.SystemName))
                    {
                        if (_repository.Any(c => c.ProductId == product.Id && c.InstanceId == instance.Id && c.ElementId == element.Id))
                        {
                            continue;
                        }

                        csv.WriteField(product.Code);
                        csv.WriteField(instance.Name);
                        csv.WriteField(element.Name);

                        csv.NextRecord();
                    }
                }
            }

            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private IQueryable<Product> FilterProducts(IQueryable<Product> products)
        {
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                products = products.Where(p => p.BrandId == currentBrand.Id);
            }

            if (_userService.IsSpecificCollection())
            {
                CollectionModel currentCollection = _userService.GetCurrentCollection();
                products = products.Where(p => p.CollectionId == currentCollection.Id);
            }

            if (_userService.IsSpecificStatus())
            {
                EntityStatusModel currentStatus = _userService.GetCurrentStatus();
                products = products.Where(p => p.StatusId == currentStatus.Id);
            }

            bool hideDrafts = _userService.HideDrafts();
            if (hideDrafts && !_userService.IsSpecificStatus())
            {
                products = products.Where(p => p.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool hideWithdrawn = _userService.HideWithdrawn();
            if (hideWithdrawn && !_userService.IsSpecificStatus())
            {
                products = products.Where(p => p.Status.SystemName != EntityStatusEnum.Withdrawn);
            }

            bool hideCopies = _userService.HideCopies();
            if (hideCopies)
            {
                products = products.Where(p => p.IsSizeCopy == false);
            }

            return products;
        }

        private void TransferValues(Copywriting toCopywriting, CopywritingModel fromCopywriting)
        {
            toCopywriting.ProductId = fromCopywriting.Product.Id;
            toCopywriting.InstanceId = fromCopywriting.Instance.Id;
            toCopywriting.ElementId = fromCopywriting.Element.Id;
            toCopywriting.Value = fromCopywriting.Value;
            toCopywriting.ByteCount = fromCopywriting.ByteCount;
            toCopywriting.HasProperLength = fromCopywriting.ByteCount <= fromCopywriting.Element.MaxByteCount;
        }

        private IQueryable<Copywriting> ApplyFilter(IQueryable<Copywriting> copywritings, ListRequest request)
        {
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                copywritings = copywritings.Where(c => c.Product.BrandId == currentBrand.Id);
            }

            if (_userService.IsSpecificCollection())
            {
                CollectionModel currentCollection = _userService.GetCurrentCollection();
                copywritings = copywritings.Where(c => c.Product.CollectionId == currentCollection.Id);
            }

            if (_userService.IsSpecificInstance())
            {
                InstanceModel currentInstance = _userService.GetCurrentInstance();
                copywritings = copywritings.Where(c => c.InstanceId == currentInstance.Id);
            }

            if (_userService.IsSpecificStatus())
            {
                EntityStatusModel currentStatus = _userService.GetCurrentStatus();
                copywritings = copywritings.Where(c => c.Product.StatusId == currentStatus.Id);
            }

            bool hideDrafts = _userService.HideDrafts();
            if (hideDrafts && !_userService.IsSpecificStatus())
            {
                copywritings = copywritings.Where(c => c.Product.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool hideWithdrawn = _userService.HideWithdrawn();
            if (hideWithdrawn && !_userService.IsSpecificStatus())
            {
                copywritings = copywritings.Where(c => c.Product.Status.SystemName != EntityStatusEnum.Withdrawn);
            }

            bool hideCopies = _userService.HideCopies();
            if (hideCopies)
            {
                copywritings = copywritings.Where(c => c.Product.IsSizeCopy == false);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return copywritings;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "product", "product_id", "instance", "instance_id", "element", "value", "byte_count", "has_proper_length" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    copywritings = searchField.Name switch
                    {
                        "id" => copywritings.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => copywritings.Where(c => c.Product.Code.Contains(searchField.Value)),
                        "product_id" => copywritings.Where(c => c.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        "instance" => copywritings.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        "instance_id" => copywritings.Where(c => c.InstanceId == searchField.Value.ParseToIntOrDefault()),
                        "element" => copywritings.Where(c => c.Element.Name.Contains(searchField.Value)),
                        "value" => copywritings.Where(c => c.Value.Contains(searchField.Value)),
                        "byte_count" => copywritings.Where(c => c.ByteCount == searchField.Value.ParseToIntOrDefault()),
                        "has_proper_length" => copywritings.Where(c => c.HasProperLength == searchField.Value.ParseToBoolOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    copywritings = copywritings.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Product.Code.Contains(searchString)
                        || c.Product.Name.Contains(searchString)
                        || c.Instance.Name.Contains(searchString)
                        || c.Element.Name.Contains(searchString)
                        || c.Value.Contains(searchString)
                        || c.ByteCount == searchString.ParseToIntOrDefault());
                }
            }

            return copywritings;
        }

        private IQueryable<Copywriting> ApplySorting(IQueryable<Copywriting> copywritings, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                copywritings = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Id) : copywritings.OrderByDescending(c => c.Id),
                    "product" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Product.Code) : copywritings.OrderByDescending(c => c.Product.Code),
                    "instance" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Instance.Name) : copywritings.OrderByDescending(c => c.Instance.Name),
                    "element" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Element.Name) : copywritings.OrderByDescending(c => c.Element.Name),
                    "value" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Value) : copywritings.OrderByDescending(c => c.Value),
                    "byte_count" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.ByteCount) : copywritings.OrderByDescending(c => c.ByteCount),
                    "has_proper_length" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.HasProperLength) : copywritings.OrderByDescending(c => c.HasProperLength),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                copywritings = copywritings.OrderBy(c => c.Id);
            }

            return copywritings;
        }

        private IQueryable<Copywriting> ApplyPaging(IQueryable<Copywriting> copywritings, ListRequest request)
        {
            return copywritings.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
