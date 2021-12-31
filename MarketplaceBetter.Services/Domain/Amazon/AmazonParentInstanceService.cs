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
    public class AmazonParentInstanceService : IAmazonParentInstanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonParentInstance> _repository;
        private readonly IMapper _mapper;

        public AmazonParentInstanceService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonParentInstance>();
            _mapper = mapper;
        }

        public AmazonParentInstanceModel Get(long id) => _mapper.Map<AmazonParentInstanceModel>(_repository.Get(id));

        public IList<AmazonParentInstanceModel> GetAll() => _mapper.Map<IList<AmazonParentInstanceModel>>(_repository.GetQuery().OrderBy(g => g.Sku));

        public IList<AmazonParentInstanceModel> GetAllForBrand(long brandId, long instanceId) => _mapper.Map<IList<AmazonParentInstanceModel>>(
                _repository.GetQuery().Where(p => p.Parent.Product.BrandId == brandId).OrderBy(p => p.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<AmazonParentInstance> parentInstances = _repository.GetQuery();

            ApplyFilter(parentInstances, request);

            return parentInstances.Count();
        }

        public IList<AmazonParentInstanceModel> GetForListRequest(ListRequest request)
        {
            IQueryable<AmazonParentInstance> parentInstances = _repository.GetQuery();

            parentInstances = ApplyFilter(parentInstances, request);
            parentInstances = ApplySorting(parentInstances, request);
            parentInstances = ApplyPaging(parentInstances, request);

            return _mapper.Map<IList<AmazonParentInstanceModel>>(parentInstances);
        }

        public void Add(AmazonParentInstanceModel parentInstance)
        {
            AmazonParentInstance parentInstanceToAdd = new();

            TransferValues(parentInstanceToAdd, parentInstance);

            _repository.Add(parentInstanceToAdd);
            _unitOfWork.Save();
        }

        public void Update(AmazonParentInstanceModel parentInstance)
        {
            AmazonParentInstance parentInstanceToUpdate = _repository.Get(parentInstance.Id);

            TransferValues(parentInstanceToUpdate, parentInstance);

            _repository.Update(parentInstanceToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(AmazonParentInstance toParentInstance, AmazonParentInstanceModel fromParentInstance)
        {
            toParentInstance.ParentId = fromParentInstance.Parent.Id;
            toParentInstance.InstanceId = fromParentInstance.Instance.Id;
            toParentInstance.Sku = fromParentInstance.Sku;
            toParentInstance.Asin = fromParentInstance.Asin;
        }

        private IQueryable<AmazonParentInstance> ApplyFilter(IQueryable<AmazonParentInstance> products, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return products;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "product", "brand", "asin" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    products = searchField.Name switch
                    {
                        "id" => products.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => products.Where(p => p.Sku.Contains(searchField.Value)),
                        "product" => products.Where(p => p.Parent.Product.Name.Contains(searchField.Value)),
                        "brand" => products.Where(p => p.Parent.Product.Brand.Name.Contains(searchField.Value)),
                        "asin" => products.Where(p => p.Asin.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    products = products.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Sku.Contains(searchString)
                        || p.Parent.Product.Name.Contains(searchString)
                        || p.Parent.Product.Brand.Name.Contains(searchString)
                        || p.Asin.Contains(searchString));
                }
            }

            return products;
        }

        private IQueryable<AmazonParentInstance> ApplySorting(IQueryable<AmazonParentInstance> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Sku) : products.OrderByDescending(p => p.Sku),
                    "product" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Parent.Product.Name) : products.OrderByDescending(p => p.Parent.Product.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Parent.Product.Brand.Name) : products.OrderByDescending(p => p.Parent.Product.Brand.Name),
                    "asin" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Asin) : products.OrderByDescending(p => p.Asin),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<AmazonParentInstance> ApplyPaging(IQueryable<AmazonParentInstance> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
