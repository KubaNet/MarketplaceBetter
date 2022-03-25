using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
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
        private readonly IParentInstanceService _parentInstanceService;
        private readonly IChildService _childService;

        public ParentService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IParentInstanceService parentInstanceService,
            IChildService childService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Parent>();
            _parentInstanceService = parentInstanceService;
            _childService = childService;
        }

        public ParentModel Get(long id) => _mapper.Map<ParentModel>(_repository.Get(id));

        public IList<ParentModel> GetAll() => _mapper.Map<IList<ParentModel>>(_repository.GetQuery().OrderBy(p => p.Sku));

        public IList<ParentModel> GetAllForBrand(long brandId) => _mapper.Map<IList<ParentModel>>(_repository.GetQuery().Where(p => p.Product.BrandId == brandId).OrderBy(p => p.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Parent> parents = _repository.GetQuery();

            ApplyFilter(parents, request);

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

            _repository.Add(parentToAdd);
            _unitOfWork.Save();

            _parentInstanceService.AddForParent(parentToAdd.Id);
            _childService.AddForParent(parentToAdd.Id);
        }

        public void Update(ParentModel parent)
        {
            Parent parentToUpdate = _repository.Get(parent.Id);

            TransferValues(parentToUpdate, parent);

            _repository.Update(parentToUpdate);
            _unitOfWork.Save();

            _parentInstanceService.AddForParent(parentToUpdate.Id);
            _childService.AddForParent(parentToUpdate.Id);
        }

        private void TransferValues(Parent toParent, ParentModel fromParent)
        {
            toParent.ProductId = fromParent.Product.Id;
            toParent.Sku = fromParent.Sku;
            toParent.ChildSku = fromParent.ChildSku;
        }

        private IQueryable<Parent> ApplyFilter(IQueryable<Parent> parents, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return parents;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "child_sku", "product", "brand" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    parents = searchField.Name switch
                    {
                        "id" => parents.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => parents.Where(p => p.Sku.Contains(searchField.Value)),
                        "child_sku" => parents.Where(p => p.ChildSku.Contains(searchField.Value)),
                        "product" => parents.Where(p => p.Product.Name.Contains(searchField.Value)),
                        "brand" => parents.Where(p => p.Product.Brand.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    parents = parents.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Sku.Contains(searchString)
                        || p.ChildSku.Contains(searchString)
                        || p.Product.Name.Contains(searchString)
                        || p.Product.Brand.Name.Contains(searchString));
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
                    "child_sku" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.ChildSku) : parents.OrderByDescending(p => p.ChildSku),
                    "product" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Product.Name) : parents.OrderByDescending(p => p.Product.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? parents.OrderBy(p => p.Product.Brand.Name) : parents.OrderByDescending(p => p.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return parents;
        }

        private IQueryable<Parent> ApplyPaging(IQueryable<Parent> parents, ListRequest request)
        {
            return parents.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
