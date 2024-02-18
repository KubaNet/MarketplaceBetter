using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
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
    public class ChildService : IChildService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Child> _repository;
        private readonly IRepository<Variant> _variantRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<EntityStatus> _statusRepository;
        private readonly IRepository<Photo> _photoRepository;
        private readonly IUserService _userService;

        public ChildService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Child>();
            _variantRepository = unitOfWork.GetRepository<Variant>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _photoRepository = unitOfWork.GetRepository<Photo>();
            _userService = userService;
        }

        public ChildModel Get(long id) => _mapper.Map<ChildModel>(_repository.Get(id));

        public ChildModel GetBySku(string sku) => _mapper.Map<ChildModel>(_repository.SingleOrDefault(c => c.Sku.Equals(sku)));

        public IList<ChildModel> GetAll() => _mapper.Map<IList<ChildModel>>(_repository.GetQuery().OrderBy(c => c.Sku));

        public IList<ChildModel> GetAllForBrand(long brandId, long instanceId) => _mapper.Map<IList<ChildModel>>(
                _repository.GetQuery().Where(c => c.Variant.Product.BrandId == brandId).OrderBy(c => c.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Child> childs = _repository.GetQuery();

            childs = ApplyFilter(childs, request);

            return childs.Count();
        }

        public IList<ChildModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Child> childs = _repository.GetQuery();

            childs = ApplyFilter(childs, request);
            childs = ApplySorting(childs, request);
            childs = ApplyPaging(childs, request);

            return _mapper.Map<IList<ChildModel>>(childs);
        }

        public void Add(ChildModel child)
        {
            Child childToAdd = new();

            TransferValues(childToAdd, child);
            childToAdd.Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft);

            _repository.Add(childToAdd);
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

                Child child = new Child
                {
                    VariantId = variantId,
                    InstanceId = instance.Id,
                    Sku = GetSkuFor(variantId, instance.Id),
                    Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft)
                };

                _repository.Add(child);
                _unitOfWork.Save();
            }
        }

        public void Update(ChildModel child)
        {
            Child childToUpdate = _repository.Get(child.Id);

            TransferValues(childToUpdate, child);

            _repository.Update(childToUpdate);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            Child child = _repository.Get(id);

            _repository.Delete(child);
            _unitOfWork.Save();
        }

        public void DeleteAllForVariant(long variantId)
        {
            IList<Child> childs = _repository.Where(c => c.VariantId == variantId).ToList();
            foreach (var child in childs)
            {
                _repository.Delete(child);
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

        public int GetPhotoCountFor(ChildModel child)
        {
            return _photoRepository.Count(p => p.VariantId == child.Variant.Id &&
                (p.InstanceId == child.Instance.Id || p.Instance.SystemName == InstanceEnum.All));
        }

        private void TransferValues(Child toChild, ChildModel fromChild)
        {
            toChild.VariantId = fromChild.Variant.Id;
            toChild.InstanceId = fromChild.Instance.Id;
            toChild.Sku = fromChild.Sku;
        }

        private IQueryable<Child> ApplyFilter(IQueryable<Child> childs, ListRequest request)
        {
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                childs = childs.Where(c => c.Variant.Product.BrandId == currentBrand.Id);
            }

            if (_userService.IsSpecificCollection())
            {
                CollectionModel currentCollection = _userService.GetCurrentCollection();
                childs = childs.Where(c => c.Variant.Product.CollectionId == currentCollection.Id);
            }

            if (_userService.IsSpecificSize())
            {
                StandardSizeModel currentSize = _userService.GetCurrentSize();
                if (currentSize.SystemName == StandardSizeEnum.OneSizePlusM)
                {
                    childs = childs.Where(c => c.Variant.Size.StandardSize.SystemName == StandardSizeEnum.OneSize || c.Variant.Size.StandardSize.SystemName == StandardSizeEnum.M);
                }
                else
                {
                    childs = childs.Where(c => c.Variant.Size.StandardSizeId == currentSize.Id);
                }
            }

            if (_userService.IsSpecificInstance())
            {
                InstanceModel currentInstance = _userService.GetCurrentInstance();
                childs = childs.Where(c => c.InstanceId == currentInstance.Id);
            }

            if (_userService.IsSpecificStatus())
            {
                EntityStatusModel currentStatus = _userService.GetCurrentStatus();
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
                string[] searchFieldNames = new[] { "id", "sku", "asins", "instance", "status", "variant", "brand", "comment", "product_id", "instance_id" };
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
                        "comment" => childs.Where(c => c.Comment.Contains(searchField.Value)),
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
                        || c.Variant.Product.Brand.Name.Contains(searchString)
                        || c.Comment.Contains(searchString));
                }
            }

            return childs;
        }

        private IQueryable<Child> ApplySorting(IQueryable<Child> childs, ListRequest request)
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
                    "comment" => request.SortDirection == SortDirection.Ascending ? childs.OrderBy(c => c.Comment) : childs.OrderByDescending(c => c.Comment),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return childs;
        }

        private IQueryable<Child> ApplyPaging(IQueryable<Child> childs, ListRequest request)
        {
            return request.ShowAll ? childs : childs.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
