using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Sales.Settings.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Settings
{
    public class ProductAccountingDataService : IProductAccountingDataService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductAccountingData> _repository;

        public ProductAccountingDataService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ProductAccountingData>();
        }
        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ProductAccountingData> data = _repository.GetQuery();

            data = ApplyFilter(data, request);

            return data.Count();
        }

        public IList<ProductAccountingDataModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ProductAccountingData> data = _repository.GetQuery();

            data = ApplyFilter(data, request);
            data = ApplySorting(data, request);
            data = ApplyPaging(data, request);

            return _mapper.Map<IList<ProductAccountingDataModel>>(data);
        }

        private IQueryable<ProductAccountingData> ApplyFilter(IQueryable<ProductAccountingData> data, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return data;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "product", "invoice_name", "commodity_code" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    data = searchField.Name switch
                    {
                        "id" => data.Where(d => d.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => data.Where(d => d.Product.Name.Contains(searchField.Value)),
                        "invoice_name" => data.Where(d => d.InvoiceName.Contains(searchField.Value)),
                        "commodity_code" => data.Where(d => d.CommodityCode.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    data = data.Where(d => d.Id == searchString.ParseToIntOrDefault()
                        || d.Product.Name.Contains(searchString)
                        || d.InvoiceName.Contains(searchString)
                        || d.CommodityCode.Contains(searchString));
                }
            }

            return data;
        }

        private IQueryable<ProductAccountingData> ApplySorting(IQueryable<ProductAccountingData> data, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                data = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? data.OrderBy(d => d.Id) : data.OrderByDescending(d => d.Id),
                    "product" => request.SortDirection == SortDirection.Ascending ? data.OrderBy(d => d.Product.Name) : data.OrderByDescending(d => d.Product.Name),
                    "invoice_name" => request.SortDirection == SortDirection.Ascending ? data.OrderBy(d => d.InvoiceName) : data.OrderByDescending(d => d.InvoiceName),
                    "commodity_code" => request.SortDirection == SortDirection.Ascending ? data.OrderBy(d => d.CommodityCode) : data.OrderByDescending(d => d.CommodityCode),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                data = data.OrderBy(d => d.Id);
            }

            return data;
        }

        private IQueryable<ProductAccountingData> ApplyPaging(IQueryable<ProductAccountingData> data, ListRequest request)
        {
            return data.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
