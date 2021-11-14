using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon;
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
    public class AmazonChildService : IAmazonChildService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonChild> _repository;
        private readonly IMapper _mapper;

        public AmazonChildService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonChild>();
            _mapper = mapper;
        }

        public AmazonChildModel Get(long id) => _mapper.Map<AmazonChildModel>(_repository.Get(id));

        public IList<AmazonChildModel> GetAll() => _mapper.Map<IList<AmazonChildModel>>(_repository.GetAll().OrderBy(g => g.Sku));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<AmazonChild> childs = _repository.GetQuery();

            ApplyFilter(childs, request);

            return childs.Count();
        }

        public IList<AmazonChildModel> GetForListRequest(ListRequest request)
        {
            IQueryable<AmazonChild> childs = _repository.GetQuery();

            childs = ApplyFilter(childs, request);
            childs = ApplySorting(childs, request);
            childs = ApplyPaging(childs, request);

            return _mapper.Map<IList<AmazonChildModel>>(childs);
        }

        public void Add(AmazonChildModel child)
        {
            AmazonChild childToAdd = new();

            TransferValues(childToAdd, child);

            _repository.Add(childToAdd);
            _unitOfWork.Save();
        }

        public void Update(AmazonChildModel child)
        {
            AmazonChild childToUpdate = _repository.Get(child.Id);

            TransferValues(childToUpdate, child);

            _repository.Update(childToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(AmazonChild toChild, AmazonChildModel fromChild)
        {
            toChild.ParentId = fromChild.Parent.Id;
            toChild.ProductVariantId = fromChild.ProductVariant.Id;
            toChild.Sku = fromChild.Sku;
            toChild.Asin = fromChild.Asin;
        }

        private IQueryable<AmazonChild> ApplyFilter(IQueryable<AmazonChild> childs, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return childs;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "sku", "instance", "parent", "product_variant", "asin" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    childs = searchField.Name switch
                    {
                        "id" => childs.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "sku" => childs.Where(c => c.Sku.Contains(searchField.Value)),
                        "instance" => childs.Where(c => c.Parent.Instance.Name.Contains(searchField.Value)),
                        "parent" => childs.Where(c => c.Parent.Sku.Contains(searchField.Value)),
                        "product_variant" => childs.Where(c => c.ProductVariant.Sku.Contains(searchField.Value)),
                        "asin" => childs.Where(c => c.Asin.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    childs = childs.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Sku.Contains(searchString)
                        || c.Parent.Instance.Name.Contains(searchString)
                        || c.Parent.Sku.Contains(searchString)
                        || c.ProductVariant.Sku.Contains(searchString)
                        || c.Asin.Contains(searchString));
                }
            }

            return childs;
        }

        private IQueryable<AmazonChild> ApplySorting(IQueryable<AmazonChild> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Id) : products.OrderByDescending(c => c.Id),
                    "sku" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Sku) : products.OrderByDescending(c => c.Sku),
                    "instance" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Parent.Instance.Name) : products.OrderByDescending(c => c.Parent.Instance.Name),
                    "parent" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Parent.Sku) : products.OrderByDescending(c => c.Parent.Sku),
                    "product_variant" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.ProductVariant.Sku) : products.OrderByDescending(c => c.ProductVariant.Sku),
                    "asin" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Asin) : products.OrderByDescending(c => c.Asin),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<AmazonChild> ApplyPaging(IQueryable<AmazonChild> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
