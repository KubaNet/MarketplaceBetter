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
    public class AmazonChildInstanceService : IAmazonChildInstanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonChildInstance> _repository;
        private readonly IRepository<Instance> _instanceRepository;

        public AmazonChildInstanceService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonChildInstance>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
        }

        public AmazonChildInstanceModel Get(long id) => _mapper.Map<AmazonChildInstanceModel>(_repository.Get(id));

        public IList<AmazonChildInstanceModel> GetAll() => _mapper.Map<IList<AmazonChildInstanceModel>>(_repository.GetQuery().OrderBy(c => c.Sku));

        public IList<AmazonChildInstanceModel> GetAllForBrand(long brandId, long instanceId) => _mapper.Map<IList<AmazonChildInstanceModel>>(
                _repository.GetQuery().Where(p => p.Child.Parent.Product.BrandId == brandId).OrderBy(p => p.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<AmazonChildInstance> ChildInstances = _repository.GetQuery();

            ApplyFilter(ChildInstances, request);

            return ChildInstances.Count();
        }

        public IList<AmazonChildInstanceModel> GetForListRequest(ListRequest request)
        {
            IQueryable<AmazonChildInstance> childInstances = _repository.GetQuery();

            childInstances = ApplyFilter(childInstances, request);
            childInstances = ApplySorting(childInstances, request);
            childInstances = ApplyPaging(childInstances, request);

            return _mapper.Map<IList<AmazonChildInstanceModel>>(childInstances);
        }

        public void Add(AmazonChildInstanceModel childInstance)
        {
            AmazonChildInstance childInstanceToAdd = new();

            TransferValues(childInstanceToAdd, childInstance);

            _repository.Add(childInstanceToAdd);
            _unitOfWork.Save();
        }

        public void AddForChild(long parentId)
        {
        }

        public void Update(AmazonChildInstanceModel childInstance)
        {
            AmazonChildInstance childInstanceToUpdate = _repository.Get(childInstance.Id);

            TransferValues(childInstanceToUpdate, childInstance);

            _repository.Update(childInstanceToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(AmazonChildInstance toChildInstance, AmazonChildInstanceModel fromChildInstance)
        {
            toChildInstance.ChildId = fromChildInstance.Child.Id;
            toChildInstance.InstanceId = fromChildInstance.Instance.Id;
            toChildInstance.Sku = fromChildInstance.Sku;
        }

        private IQueryable<AmazonChildInstance> ApplyFilter(IQueryable<AmazonChildInstance> products, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return products;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "instance", "child", "brand" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    products = searchField.Name switch
                    {
                        "id" => products.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => products.Where(p => p.Sku.Contains(searchField.Value)),
                        "instance" => products.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "child" => products.Where(p => p.Child.Sku.Contains(searchField.Value)),
                        "brand" => products.Where(p => p.Child.Parent.Product.Brand.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    products = products.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Sku.Contains(searchString)
                        || p.Instance.Name.Contains(searchString)
                        || p.Child.Sku.Contains(searchString)
                        || p.Child.Parent.Product.Brand.Name.Contains(searchString));
                }
            }

            return products;
        }

        private IQueryable<AmazonChildInstance> ApplySorting(IQueryable<AmazonChildInstance> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Sku) : products.OrderByDescending(p => p.Sku),
                    "instance" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Instance.Name) : products.OrderByDescending(p => p.Instance.Name),
                    "child" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Child.Sku) : products.OrderByDescending(p => p.Child.Sku),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Child.Parent.Product.Brand.Name) : products.OrderByDescending(p => p.Child.Parent.Product.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<AmazonChildInstance> ApplyPaging(IQueryable<AmazonChildInstance> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
