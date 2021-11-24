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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonParent> _repository;
        private readonly IMapper _mapper;

        public AmazonParentService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonParent>();
            _mapper = mapper;
        }

        public AmazonParentModel Get(long id) => _mapper.Map<AmazonParentModel>(_repository.Get(id));

        public IList<AmazonParentModel> GetAll() => _mapper.Map<IList<AmazonParentModel>>(_repository.GetQuery().OrderBy(g => g.Sku));

        public IList<AmazonParentModel> GetAllForBrandAndInstance(long brandId, long instanceId) => _mapper.Map<IList<AmazonParentModel>>(
                _repository.GetQuery().Where(p => p.Product.BrandId == brandId && p.InstanceId == instanceId).OrderBy(p => p.Sku));

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
        }

        public void Update(AmazonParentModel parent)
        {
            AmazonParent parentToUpdate = _repository.Get(parent.Id);

            TransferValues(parentToUpdate, parent);

            _repository.Update(parentToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(AmazonParent toParent, AmazonParentModel fromParent)
        {
            toParent.ProductId = fromParent.Product.Id;
            toParent.InstanceId = fromParent.Instance.Id;
            toParent.Sku = fromParent.Sku;
            toParent.Asin = fromParent.Asin;
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
                string[] searchFieldNames = new[] { "id", "sku", "instance", "product", "brand", "asin", "child_sku" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    products = searchField.Name switch
                    {
                        "id" => products.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => products.Where(p => p.Sku.Contains(searchField.Value)),
                        "instance" => products.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "product" => products.Where(p => p.Product.Name.Contains(searchField.Value)),
                        "brand" => products.Where(p => p.Product.Brand.Name.Contains(searchField.Value)),
                        "asin" => products.Where(p => p.Asin.Contains(searchField.Value)),
                        "child_sku" => products.Where(p => p.ChildSku.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    products = products.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Sku.Contains(searchString)
                        || p.Instance.Name.Contains(searchString)
                        || p.Product.Name.Contains(searchString)
                        || p.Product.Brand.Name.Contains(searchString)
                        || p.Asin.Contains(searchString)
                        || p.ChildSku.Contains(searchString));
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
                    "instance" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Instance.Name) : products.OrderByDescending(p => p.Instance.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Product.Name) : products.OrderByDescending(p => p.Product.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Product.Brand.Name) : products.OrderByDescending(p => p.Product.Brand.Name),
                    "asin" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Asin) : products.OrderByDescending(p => p.Asin),
                    "child_sku" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.ChildSku) : products.OrderByDescending(p => p.ChildSku),
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
