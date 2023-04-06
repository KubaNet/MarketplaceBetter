using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Infrastructure.Helpers;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class ChildInstanceService : IChildInstanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ChildInstance> _repository;
        private readonly IRepository<Child> _childRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<EntityStatus> _statusRepository;
        private readonly ICurrentBrandService _currentBrandService;
        private readonly ICurrentInstanceService _currentInstanceService;

        public ChildInstanceService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICurrentBrandService currentBrandService,
            ICurrentInstanceService currentInstanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ChildInstance>();
            _childRepository = unitOfWork.GetRepository<Child>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _currentBrandService = currentBrandService;
            _currentInstanceService = currentInstanceService;
        }

        public ChildInstanceModel Get(long id) => _mapper.Map<ChildInstanceModel>(_repository.Get(id));

        public IList<ChildInstanceModel> GetAll() => _mapper.Map<IList<ChildInstanceModel>>(_repository.GetQuery().OrderBy(c => c.Sku));

        public IList<ChildInstanceModel> GetAllForBrand(long brandId, long instanceId) => _mapper.Map<IList<ChildInstanceModel>>(
                _repository.GetQuery().Where(c => c.Child.Parent.Product.BrandId == brandId).OrderBy(c => c.Sku));

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

        public void AddForChild(long childId)
        {
            foreach (var instance in _instanceRepository.Where(i => i.IsNormal).OrderBy(i => i.Id))
            {
                if (_repository.Any(c => c.ChildId == childId && c.InstanceId == instance.Id))
                {
                    continue;
                }

                ChildInstance childInstance = new ChildInstance
                {
                    ChildId = childId,
                    InstanceId = instance.Id,
                    Sku = GetSkuFor(childId, instance.Id),
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

        public void DeleteAllForChild(long childId)
        {
            IList<ChildInstance> childInstances = _repository.Where(c => c.ChildId == childId).ToList();
            foreach (var childInstance in childInstances)
            {
                _repository.Delete(childInstance);
            }

            _unitOfWork.Save();
        }

        public void ChangeStatus(IList<ChildInstanceModel> childInstances, EntityStatusModel status)
        {
            foreach (var childInstance in childInstances)
            {
                ChildInstance childToUpdate = _repository.Get(childInstance.Id);

                childToUpdate.StatusId = status.Id;
                _repository.Update(childToUpdate);
            }

            _unitOfWork.Save();
        }

        public string GetSkuFor(long? childId, long? instanceId)
        {
            if (childId.HasValue && instanceId.HasValue)
            {
                Child child = _childRepository.Get(childId.Value);
                Instance instance = _instanceRepository.Get(instanceId.Value);

                return $"{InstanceHelper.GetCodeFor(instance.SystemName)}_{child.Sku}";
            }
            else if (childId.HasValue)
            {
                Child child = _childRepository.Get(childId.Value);

                return child.Sku;
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
            toChildInstance.ChildId = fromChildInstance.Child.Id;
            toChildInstance.InstanceId = fromChildInstance.Instance.Id;
            toChildInstance.Sku = fromChildInstance.Sku;
        }

        private IQueryable<ChildInstance> ApplyFilter(IQueryable<ChildInstance> childs, ListRequest request)
        {
            if (_currentBrandService.IsSpecificBrand())
            {
                childs = childs.Where(c => c.Child.Parent.Product.BrandId == _currentBrandService.GetCurrentBrand().Id);
            }
            if (_currentInstanceService.IsSpecificInstance())
            {
                childs = childs.Where(c => c.InstanceId == _currentInstanceService.GetCurrentInstance().Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return childs;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "instance", "status", "child", "brand", "product_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    childs = searchField.Name switch
                    {
                        "id" => childs.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => childs.Where(c => c.Sku.Contains(searchField.Value)),
                        "instance" => childs.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        "status" => childs.Where(c => c.Status.Name.Contains(searchField.Value)),
                        "child" => childs.Where(c => c.Child.Sku.Contains(searchField.Value)),
                        "brand" => childs.Where(c => c.Child.Parent.Product.Brand.Name.Contains(searchField.Value)),
                        "product_id" => childs.Where(c => c.Child.Parent.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    childs = childs.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Sku.Contains(searchString)
                        || c.Instance.Name.Contains(searchString)
                        || c.Status.Name.Contains(searchString)
                        || c.Child.Sku.Contains(searchString)
                        || c.Child.Parent.Product.Brand.Name.Contains(searchString));
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
                    "instance" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Instance.Name) : childs.OrderByDescending(c => c.Instance.Name),
                    "status" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Status.Name) : childs.OrderByDescending(c => c.Status.Name),
                    "child" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Child.Sku) : childs.OrderByDescending(c => c.Child.Sku),
                    "brand" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Child.Parent.Product.Brand.Name) : childs.OrderByDescending(c => c.Child.Parent.Product.Brand.Name),
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
