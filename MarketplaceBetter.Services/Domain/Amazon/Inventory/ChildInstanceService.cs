using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
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
using Microsoft.VisualBasic;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class ChildInstanceService : IChildInstanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ChildInstance> _repository;
        private readonly IRepository<Variant> _variantRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<EntityStatus> _statusRepository;
        private readonly IUserService _userService;

        public ChildInstanceService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ChildInstance>();
            _variantRepository = unitOfWork.GetRepository<Variant>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _userService = userService;
        }

        public ChildInstanceModel Get(long id) => _mapper.Map<ChildInstanceModel>(_repository.Get(id));

        public IList<ChildInstanceModel> GetAll() => _mapper.Map<IList<ChildInstanceModel>>(_repository.GetQuery().OrderBy(c => c.Sku));

        public IList<ChildInstanceModel> GetAllForBrand(long brandId, long instanceId) => _mapper.Map<IList<ChildInstanceModel>>(
                _repository.GetQuery().Where(c => c.Variant.Product.BrandId == brandId).OrderBy(c => c.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ChildInstance> childInstances = _repository.GetQuery();

            childInstances = ApplyFilter(childInstances, request);

            return childInstances.Count();
        }

        public IList<ChildInstanceModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ChildInstance> childInstances = _repository.GetQuery();

            childInstances = ApplyFilter(childInstances, request);
            childInstances = ApplySorting(childInstances, request);
            childInstances = ApplyPaging(childInstances, request);

            return _mapper.Map<IList<ChildInstanceModel>>(childInstances);
        }

        public void Add(ChildInstanceModel childInstance)
        {
            ChildInstance childInstanceToAdd = new();

            TransferValues(childInstanceToAdd, childInstance);
            childInstanceToAdd.Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft);

            _repository.Add(childInstanceToAdd);
            _unitOfWork.Save();
        }

        public void AddForVariant(long variantId)
        {
            foreach (var instance in _instanceRepository.Where(i => i.IsNormal).OrderBy(i => i.Id))
            {
                if (_repository.Any(c => c.VariantId == variantId && c.InstanceId == instance.Id))
                {
                    continue;
                }

                ChildInstance childInstance = new ChildInstance
                {
                    VariantId = variantId,
                    InstanceId = instance.Id,
                    Sku = GetSkuFor(variantId, instance.Id),
                    Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft)
                };

                _repository.Add(childInstance);
                _unitOfWork.Save();
            }
        }

        public void Update(ChildInstanceModel childInstance)
        {
            ChildInstance childInstanceToUpdate = _repository.Get(childInstance.Id);

            TransferValues(childInstanceToUpdate, childInstance);

            _repository.Update(childInstanceToUpdate);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            ChildInstance child = _repository.Get(id);

            _repository.Delete(child);
            _unitOfWork.Save();
        }

        public void DeleteAllForVariant(long variantId)
        {
            IList<ChildInstance> childInstances = _repository.Where(c => c.VariantId == variantId).ToList();
            foreach (var childInstance in childInstances)
            {
                _repository.Delete(childInstance);
            }

            _unitOfWork.Save();
        }

        public string GetSkuFor(long? variantId, long? instanceId)
        {
            if (variantId.HasValue && instanceId.HasValue)
            {
                Variant variant = _variantRepository.Get(variantId.Value);
                Instance instance = _instanceRepository.Get(instanceId.Value);

                return $"{InstanceHelper.GetCodeFor(instance.SystemName)}_{variant.Sku}";
            }
            else if (variantId.HasValue)
            {
                Variant variant = _variantRepository.Get(variantId.Value);

                return variant.Sku;
            }
            else if (instanceId.HasValue)
            {
                Instance instance = _instanceRepository.Get(instanceId.Value);

                return $"{InstanceHelper.GetCodeFor(instance.SystemName)}_";
            }

            return null;
        }

        private void TransferValues(ChildInstance toChildInstance, ChildInstanceModel fromChildInstance)
        {
            toChildInstance.VariantId = fromChildInstance.Variant.Id;
            toChildInstance.InstanceId = fromChildInstance.Instance.Id;
            toChildInstance.Sku = fromChildInstance.Sku;
        }

        private IQueryable<ChildInstance> ApplyFilter(IQueryable<ChildInstance> childs, ListRequest request)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (_userService.IsSpecificBrand())
            {
                childs = childs.Where(c => c.Variant.Product.BrandId == currentBrand.Id);
            }

            InstanceModel currentInstance = _userService.GetCurrentInstance();
            if (_userService.IsSpecificInstance())
            {
                childs = childs.Where(c => c.InstanceId == currentInstance.Id);
            }

            EntityStatusModel currentStatus = _userService.GetCurrentStatus();
            if (_userService.IsSpecificStatus())
            {
                childs = childs.Where(c => c.StatusId == currentStatus.Id);
            }

            bool hideDrafts = _userService.HideDrafts();
            if (hideDrafts && !_userService.IsSpecificStatus())
            {
                childs = childs.Where(c => c.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool hideWithdrawn = _userService.HideWithdrawn();
            if (hideWithdrawn && !_userService.IsSpecificStatus())
            {
                childs = childs.Where(c => c.Status.SystemName != EntityStatusEnum.Withdrawn);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return childs;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "asins", "instance", "status", "variant", "brand", "product_id", "instance_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    childs = searchField.Name switch
                    {
                        "id" => childs.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => childs.Where(c => c.Sku.Contains(searchField.Value)),
                        "asin" => childs.Where(c => c.Variant.Asin.Contains(searchField.Value)),
                        "instance" => childs.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        "status" => childs.Where(c => c.Status.Name.Contains(searchField.Value)),
                        "variant" => childs.Where(c => c.Variant.Sku.Contains(searchField.Value)),
                        "brand" => childs.Where(c => c.Variant.Product.Brand.Name.Contains(searchField.Value)),
                        "product_id" => childs.Where(c => c.Variant.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        "instance_id" => childs.Where(c => c.Instance.Id == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    childs = childs.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Sku.Contains(searchString)
                        || c.Variant.Asin.Contains(searchString)
                        || c.Instance.Name.Contains(searchString)
                        || c.Status.Name.Contains(searchString)
                        || c.Variant.Sku.Contains(searchString)
                        || c.Variant.Product.Brand.Name.Contains(searchString));
                }
            }

            return childs;
        }

        private IQueryable<ChildInstance> ApplySorting(IQueryable<ChildInstance> childs, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                childs = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Id) : childs.OrderByDescending(c => c.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Sku) : childs.OrderByDescending(c => c.Sku),
                    "asin" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Variant.Asin) : childs.OrderByDescending(c => c.Variant.Asin),
                    "instance" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Instance.Name) : childs.OrderByDescending(c => c.Instance.Name),
                    "status" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Status.Name) : childs.OrderByDescending(c => c.Status.Name),
                    "variant" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Variant.Sku) : childs.OrderByDescending(c => c.Variant.Sku),
                    "brand" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Variant.Product.Brand.Name) : childs.OrderByDescending(c => c.Variant.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return childs;
        }

        private IQueryable<ChildInstance> ApplyPaging(IQueryable<ChildInstance> childs, ListRequest request)
        {
            return request.ShowAll ? childs : childs.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
