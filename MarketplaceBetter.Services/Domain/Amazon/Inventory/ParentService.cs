using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
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
    public class ParentService : IParentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Parent> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<EntityStatus> _statusRepository;
        private readonly IUserService _userService;

        public ParentService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Parent>();
            _productRepository = unitOfWork.GetRepository<Product>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _userService = userService;
        }

        public ParentModel Get(long id) => _mapper.Map<ParentModel>(_repository.Get(id));

        public IList<ParentModel> GetAll() => _mapper.Map<IList<ParentModel>>(_repository.GetQuery().OrderBy(p => p.Sku));

        public IList<ParentModel> GetAllForBrand(long brandId, long instanceId) => _mapper.Map<IList<ParentModel>>(
                _repository.GetQuery().Where(p => p.Product.BrandId == brandId).OrderBy(p => p.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Parent> parents = _repository.GetQuery();

            parents = ApplyFilter(parents, request);

            return parents.Count();
        }

        public IList<ParentModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Parent> parents = _repository.GetQuery();

            parents = ApplyFilter(parents, request);
            parents = ApplySorting(parents, request);
            parents = ApplyPaging(parents, request);

            return _mapper.Map<IList<ParentModel>>(parents);
        }

        public void Add(ParentModel parent)
        {
            Parent parentToAdd = new();

            TransferValues(parentToAdd, parent);
            parentToAdd.Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft);

            _repository.Add(parentToAdd);
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

                Parent parent = new Parent 
                { 
                    ProductId = productId, 
                    InstanceId = instance.Id, 
                    Sku = GetSkuFor(productId, instance.Id),
                    Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft)
                };

                _repository.Add(parent);
                _unitOfWork.Save();
            }
        }

        public void Update(ParentModel parent)
        {
            Parent parentToUpdate = _repository.Get(parent.Id);

            TransferValues(parentToUpdate, parent);

            _repository.Update(parentToUpdate);
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

        private void TransferValues(Parent toParent, ParentModel fromParent)
        {
            toParent.ProductId = fromParent.Product.Id;
            toParent.InstanceId = fromParent.Instance.Id;
            toParent.Sku = fromParent.Sku;
            toParent.Asin = fromParent.Asin;
            toParent.Category = fromParent.Category;
        }

        private IQueryable<Parent> ApplyFilter(IQueryable<Parent> parents, ListRequest request)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (_userService.IsSpecificBrand())
            {
                parents = parents.Where(p => p.Product.BrandId == currentBrand.Id);
            }

            CollectionModel currentCollection = _userService.GetCurrentCollection();
            if (_userService.IsSpecificCollection())
            {
                parents = parents.Where(p => p.Product.CollectionId == currentCollection.Id);
            }

            InstanceModel currentInstance = _userService.GetCurrentInstance();
            if (_userService.IsSpecificInstance())
            {
                parents = parents.Where(p => p.InstanceId == currentInstance.Id);
            }

            EntityStatusModel currentStatus = _userService.GetCurrentStatus();
            if (_userService.IsSpecificStatus())
            {
                parents = parents.Where(p => p.StatusId == currentStatus.Id);
            }

            bool hideDrafts = _userService.HideDrafts();
            if (hideDrafts && !_userService.IsSpecificStatus())
            {
                parents = parents.Where(p => p.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool hideWithdrawn = _userService.HideWithdrawn();
            if (hideWithdrawn && !_userService.IsSpecificStatus())
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
                string[] searchFieldNames = new[] { "id", "sku", "asin", "instance", "status", "code", "brand", "template", "category", "comment", "product_id" };
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
                        "code" => parents.Where(p => p.Product.Code.Contains(searchField.Value)),
                        "brand" => parents.Where(p => p.Product.Brand.Name.Contains(searchField.Value)),
                        "template" => parents.Where(p => p.Template.FileName.Contains(searchField.Value)),
                        "category" => parents.Where(p => p.Category.Contains(searchField.Value)),
                        "comment" => parents.Where(p => p.Comment.Contains(searchField.Value)),
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
                        || p.Product.Code.Contains(searchString)
                        || p.Product.Brand.Name.Contains(searchString)
                        || p.Template.FileName.Contains(searchString)
                        || p.Category.Contains(searchString)
                        || p.Comment.Contains(searchString));
                }
            }

            return parents;
        }

        private IQueryable<Parent> ApplySorting(IQueryable<Parent> parents, ListRequest request)
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
                    "product" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Product.Code) : parents.OrderByDescending(p => p.Product.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Product.Brand.Name) : parents.OrderByDescending(p => p.Product.Brand.Name),
                    "template" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Template.FileName) : parents.OrderByDescending(p => p.Template.FileName),
                    "category" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Category) : parents.OrderByDescending(p => p.Category),
                    "comment" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Comment) : parents.OrderByDescending(p => p.Comment),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return parents;
        }

        private IQueryable<Parent> ApplyPaging(IQueryable<Parent> parents, ListRequest request)
        {
            return request.ShowAll ? parents : parents.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
