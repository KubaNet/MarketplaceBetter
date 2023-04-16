using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Infrastructure.Helpers;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class ParentInstanceService : IParentInstanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ParentInstance> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<EntityStatus> _statusRepository;
        private readonly IUserService _userService;

        public ParentInstanceService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ParentInstance>();
            _productRepository = unitOfWork.GetRepository<Product>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _userService = userService;
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

        private void TransferValues(ParentInstance toParent, ParentInstanceModel fromParent)
        {
            toParent.ProductId = fromParent.Product.Id;
            toParent.InstanceId = fromParent.Instance.Id;
            toParent.Sku = fromParent.Sku;
            toParent.Asin = fromParent.Asin;
        }

        private IQueryable<ParentInstance> ApplyFilter(IQueryable<ParentInstance> parents, ListRequest request)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (currentBrand != null && _userService.IsSpecificBrand())
            {
                parents = parents.Where(p => p.Product.BrandId == currentBrand.Id);
            }

            InstanceModel currentInstance = _userService.GetCurrentInstance();
            if (currentInstance != null && _userService.IsSpecificInstance())
            {
                parents = parents.Where(p => p.InstanceId == currentInstance.Id);
            }

            bool showDrafts = _userService.ShowDrafts();
            if (!showDrafts)
            {
                parents = parents.Where(p => p.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool showWithdrawn = _userService.ShowWithdrawn();
            if (!showWithdrawn)
            {
                parents = parents.Where(p => p.Status.SystemName != EntityStatusEnum.Withdrawn);
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
