using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon
{
    public class AmazonParentService : IAmazonParentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonParent> _repository;
        private readonly IAmazonParentInstanceService _amazonParentInstanceService;

        public AmazonParentService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IAmazonParentInstanceService amazonParentInstanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonParent>();
            _amazonParentInstanceService = amazonParentInstanceService;
        }

        public AmazonParentModel Get(long id) => _mapper.Map<AmazonParentModel>(_repository.Get(id));

        public IList<AmazonParentModel> GetAll() => _mapper.Map<IList<AmazonParentModel>>(_repository.GetQuery().OrderBy(p => p.Sku));

        public IList<AmazonParentModel> GetAllForBrand(long brandId) => _mapper.Map<IList<AmazonParentModel>>(_repository.GetQuery().Where(p => p.Product.BrandId == brandId).OrderBy(p => p.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<AmazonParent> parents = _repository.GetQuery();

            ApplyFilter(parents, request);

            return parents.Count();
        }

        public IList<AmazonParentModel> GetForListRequest(ListRequest request)
        {
            IQueryable<AmazonParent> parents = _repository.GetQuery();

            parents = ApplyFilter(parents, request);
            parents = ApplySorting(parents, request);
            parents = ApplyPaging(parents, request);

            return _mapper.Map<IList<AmazonParentModel>>(parents);
        }

        public void Add(AmazonParentModel parent)
        {
            AmazonParent parentToAdd = new();

            TransferValues(parentToAdd, parent);

            _repository.Add(parentToAdd);
            _unitOfWork.Save();

            _amazonParentInstanceService.AddForParent(parentToAdd.Id);
        }

        public void Update(AmazonParentModel parent)
        {
            AmazonParent parentToUpdate = _repository.Get(parent.Id);

            TransferValues(parentToUpdate, parent);

            _repository.Update(parentToUpdate);
            _unitOfWork.Save();

            _amazonParentInstanceService.AddForParent(parentToUpdate.Id);
        }

        private void TransferValues(AmazonParent toParent, AmazonParentModel fromParent)
        {
            toParent.ProductId = fromParent.Product.Id;
            toParent.Sku = fromParent.Sku;
            toParent.ChildSku = fromParent.ChildSku;
        }

        private IQueryable<AmazonParent> ApplyFilter(IQueryable<AmazonParent> products, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return products;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "child_sku", "product", "brand" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    products = searchField.Name switch
                    {
                        "id" => products.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => products.Where(p => p.Sku.Contains(searchField.Value)),
                        "child_sku" => products.Where(p => p.ChildSku.Contains(searchField.Value)),
                        "product" => products.Where(p => p.Product.Name.Contains(searchField.Value)),
                        "brand" => products.Where(p => p.Product.Brand.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    products = products.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Sku.Contains(searchString)
                        || p.ChildSku.Contains(searchString)
                        || p.Product.Name.Contains(searchString)
                        || p.Product.Brand.Name.Contains(searchString));
                }
            }

            return products;
        }

        private IQueryable<AmazonParent> ApplySorting(IQueryable<AmazonParent> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Sku) : products.OrderByDescending(p => p.Sku),
                    "child_sku" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.ChildSku) : products.OrderByDescending(p => p.ChildSku),
                    "product" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Product.Name) : products.OrderByDescending(p => p.Product.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Product.Brand.Name) : products.OrderByDescending(p => p.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<AmazonParent> ApplyPaging(IQueryable<AmazonParent> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
