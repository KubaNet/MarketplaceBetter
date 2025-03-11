using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Infrastructure.Helpers;
using MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents
{
    public class APlusContentService : IAPlusContentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<APlusContent> _repository;
        private readonly IRepository<Variant> _variantRepository;
        private readonly IRepository<EntityStatus> _statusRepository;
        private readonly IRepository<Child> _childRepository;
        private readonly IUserService _userService;
        private readonly IAPlusModuleValueService _moduleService;

        public APlusContentService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService,
            IAPlusModuleValueService moduleService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<APlusContent>();
            _variantRepository = unitOfWork.GetRepository<Variant>();
            _statusRepository = unitOfWork.GetRepository<EntityStatus>();
            _childRepository = unitOfWork.GetRepository<Child>();
            _userService = userService;
            _moduleService = moduleService;
        }

        public APlusContentModel Get(long id) => _mapper.Map<APlusContentModel>(_repository.Get(id));

        public IList<APlusContentModel> GetAll(bool onlyCurrent)
        {
            if (onlyCurrent && _userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                return _mapper.Map<IList<APlusContentModel>>(_repository.Where(c => c.Product.Brand.Id == currentBrand.Id).OrderBy(c => c.Name));
            }
            else
            {
                return _mapper.Map<IList<APlusContentModel>>(_repository.GetQuery().OrderBy(c => c.Name));
            }
        }

        public IList<APlusContentModel> GetAllFor(BrandModel brand, ProductModel product, InstanceModel instance)
        {
            IQueryable<APlusContent> contents = _repository.GetQuery();

            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                contents = contents.Where(c => c.Product.BrandId == currentBrand.Id);
            }

            if (brand != null)
            {
                contents = contents.Where(c => c.Product.BrandId == brand.Id);
            }

            if (product != null)
            {
                contents = contents.Where(c => c.ProductId == product.Id);
            }

            if (instance != null)
            {
                contents = contents.Where(c => c.InstanceId == instance.Id);
            }

            return _mapper.Map<IList<APlusContentModel>>(contents.OrderBy(c => c.Name));
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<APlusContent> contents = _repository.GetQuery();

            contents = ApplyFilter(contents, request);

            return contents.Count();
        }

        public IList<APlusContentModel> GetForListRequest(ListRequest request)
        {
            IQueryable<APlusContent> contents = _repository.GetQuery();

            contents = ApplyFilter(contents, request);
            contents = ApplySorting(contents, request);
            contents = ApplyPaging(contents, request);

            return _mapper.Map<IList<APlusContentModel>>(contents);
        }

        public void Add(APlusContentModel content, IList<string> variantsSkus)
        {
            APlusContent contentToAdd = new();

            TransferValues(contentToAdd, content, variantsSkus);
            contentToAdd.Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft);

            _repository.Add(contentToAdd);
            _unitOfWork.Save();
        }

        public void AddForChilds(IList<long> childsIds)
        {
            foreach (var childId in childsIds)
            {
                Child child = _childRepository.Get(childId);
                Variant variant = child.Variant;

                if (_repository.Any(c => c.InstanceId == child.InstanceId && c.Variants.Count == 1 && c.Variants.Any(v => v.VariantId == variant.Id)))
                {
                    continue;
                }

                APlusContent content = new APlusContent
                {
                    ProductId = variant.ProductId,
                    InstanceId = child.InstanceId,
                    Status = _statusRepository.Single(s => s.SystemName == EntityStatusEnum.Draft),
                    Name = $"{InstanceHelper.GetCodeFor(child.Instance.SystemName)}_{variant.Product.Brand.Code.ToLower()}_{variant.Product.Code}_{variant.Color.Code}_{variant.Size.Code}",
                    Variants = new List<APlusContentVariant>() { new APlusContentVariant { Variant = variant } }
                };

                _repository.Add(content);
            }
            _unitOfWork.Save();
        }

        public void Update(APlusContentModel content, IList<string> variantsSkus)
        {
            APlusContent contentToUpdate = _repository.Get(content.Id);

            TransferValues(contentToUpdate, content, variantsSkus);

            _repository.Update(contentToUpdate);
            _unitOfWork.Save();
        }

        public void Delete(long id)
        {
            APlusContent content = _repository.Get(id);

            IList<long> modulesIds = content.Modules.Select(s => s.Id).ToList();
            foreach (var moduleId in modulesIds)
            {
                _moduleService.Delete(moduleId);
            }

            _repository.Delete(content);
            _unitOfWork.Save();
        }


        private void TransferValues(APlusContent toContent, APlusContentModel fromContent, IList<string> variantsSkus)
        {
            toContent.Name = fromContent.Name;
            toContent.ProductId = fromContent.Product.Id;
            toContent.InstanceId = fromContent.Instance.Id;
            toContent.AllVariants = fromContent.AllVariants;

            TransferVariants(toContent.Variants, variantsSkus);
        }

        private void TransferVariants(IList<APlusContentVariant> variants, IList<string> variantsSkus)
        {
            IList<APlusContentVariant> variantsToRemove = new List<APlusContentVariant>();
            foreach (var contentVariant in variants)
            {
                if (!variantsSkus.Contains(contentVariant.Variant.Sku))
                {
                    variantsToRemove.Add(contentVariant);
                }
            }

            foreach (var variantToRemove in variantsToRemove)
            {
                variants.Remove(variantToRemove);
            }

            foreach (var variantSku in variantsSkus)
            {
                Variant variant = _variantRepository.Single(v => v.Sku == variantSku);

                if (!variants.Any(v => v.Variant.Id == variant.Id))
                {
                    APlusContentVariant contentVariant = new APlusContentVariant { Variant = variant };

                    variants.Add(contentVariant);
                }
            }
        }

        private IQueryable<APlusContent> ApplyFilter(IQueryable<APlusContent> contents, ListRequest request)
        {
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                contents = contents.Where(c => c.Product.BrandId == currentBrand.Id);
            }

            if (_userService.IsSpecificCollection())
            {
                CollectionModel currentCollection = _userService.GetCurrentCollection();
                contents = contents.Where(c => c.Product.CollectionId == currentCollection.Id);
            }

            if (_userService.IsSpecificSize())
            {
                StandardSizeModel currentSize = _userService.GetCurrentSize();
                if (currentSize.SystemName == StandardSizeEnum.OneSizePlusM)
                {
                    contents = contents.Where(c => !c.Variants.Any() || c.Variants.Any(v => v.Variant.Size.StandardSize.SystemName == StandardSizeEnum.OneSize || v.Variant.Size.StandardSize.SystemName == StandardSizeEnum.M));
                }
                else if (currentSize.SystemName == StandardSizeEnum.NotOneSize)
                {
                    contents = contents.Where(c => !c.Variants.Any() || c.Variants.Any(v => v.Variant.Size.StandardSize.SystemName != StandardSizeEnum.OneSize));
                }
                else
                {
                    contents = contents.Where(c => !c.Variants.Any() || c.Variants.Any(v => v.Variant.Size.StandardSizeId == currentSize.Id));
                }
            }

            if (_userService.IsSpecificInstance())
            {
                InstanceModel currentInstance = _userService.GetCurrentInstance();
                contents = contents.Where(c => c.InstanceId == currentInstance.Id);
            }

            if (_userService.IsSpecificStatus())
            {
                EntityStatusModel currentStatus = _userService.GetCurrentStatus();
                contents = contents.Where(c => c.StatusId == currentStatus.Id);
            }

            bool hideDrafts = _userService.HideDrafts();
            if (hideDrafts && !_userService.IsSpecificStatus())
            {
                contents = contents.Where(c => c.Status.SystemName != EntityStatusEnum.Draft);
            }

            bool hideWithdrawn = _userService.HideWithdrawn();
            if (hideWithdrawn && !_userService.IsSpecificStatus())
            {
                contents = contents.Where(c => c.Status.SystemName != EntityStatusEnum.Withdrawn);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return contents;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "status", "product", "product_id", "variant", "brand", "instance" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    contents = searchField.Name switch
                    {
                        "id" => contents.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => contents.Where(c => c.Name.Contains(searchField.Value)),
                        "status" => contents.Where(c => c.Status.Name.Contains(searchField.Value)),
                        "product" => contents.Where(c => c.Product.Code.Contains(searchField.Value)),
                        "product_id" => contents.Where(c => c.Product.Id == searchField.Value.ParseToIntOrDefault()),
                        "variant" => contents.Where(c => c.Variants.Any(v => v.Variant.Sku.Contains(searchField.Value) || (v.Variant.Asin != null && v.Variant.Asin.Contains(searchField.Value)))),
                        "brand" => contents.Where(c => c.Product.Brand.Name.Contains(searchField.Value)),
                        "instance" => contents.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    contents = contents.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Name.Contains(searchString)
                        || c.Status.Name.Contains(searchString)
                        || c.Product.Code.Contains(searchString)
                        || c.Variants.Any(v => v.Variant.Sku.Contains(searchString) || v.Variant.Asin.Contains(searchString))
                        || c.Product.Brand.Name.Contains(searchString)
                        || c.Instance.Name.Contains(searchString));
                }
            }

            return contents;
        }

        private IQueryable<APlusContent> ApplySorting(IQueryable<APlusContent> contents, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                contents = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Id) : contents.OrderByDescending(c => c.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Name) : contents.OrderByDescending(c => c.Name),
                    "status" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Status.Name) : contents.OrderByDescending(c => c.Status.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Product.Code) : contents.OrderByDescending(c => c.Product.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Product.Brand.Name) : contents.OrderByDescending(c => c.Product.Brand.Name),
                    "instance" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Instance.Name) : contents.OrderByDescending(c => c.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                contents = contents.OrderBy(c => c.Name);
            }

            return contents;
        }

        private IQueryable<APlusContent> ApplyPaging(IQueryable<APlusContent> contents, ListRequest request)
        {
            return contents.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
