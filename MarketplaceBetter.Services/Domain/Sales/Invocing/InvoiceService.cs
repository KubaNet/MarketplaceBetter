using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.Invoicing;
using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Sales.Invocing.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Invocing
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Invoice> _repository;

        public InvoiceService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Invoice>();
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Invoice> invoices = _repository.GetQuery();

            invoices = ApplyFilter(invoices, request);

            return invoices.Count();
        }

        public IList<InvoiceModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Invoice> invoices = _repository.GetQuery();

            invoices = ApplyFilter(invoices, request);
            invoices = ApplySorting(invoices, request);
            invoices = ApplyPaging(invoices, request);

            return _mapper.Map<IList<InvoiceModel>>(invoices);
        }

        private IQueryable<Invoice> ApplyFilter(IQueryable<Invoice> invoices, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return invoices;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "number", "order_id" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    invoices = searchField.Name switch
                    {
                        "id" => invoices.Where(i => i.Id == searchField.Value.ParseToIntOrDefault()),
                        "number" => invoices.Where(i => i.Number.Contains(searchField.Value)),
                        "order_id" => invoices.Where(i => i.OrderId.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    invoices = invoices.Where(i => i.Id == searchString.ParseToIntOrDefault()
                        || i.Number.Contains(searchString)
                        || i.OrderId.Contains(searchString));
                }
            }

            return invoices;
        }

        private IQueryable<Invoice> ApplySorting(IQueryable<Invoice> invoices, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                invoices = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.Id) : invoices.OrderByDescending(i => i.Id),
                    "number" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.Number) : invoices.OrderByDescending(i => i.Number),
                    "order_id" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.OrderId) : invoices.OrderByDescending(i => i.OrderId),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                invoices = invoices.OrderBy(i => i.Id);
            }

            return invoices;
        }

        private IQueryable<Invoice> ApplyPaging(IQueryable<Invoice> invoices, ListRequest request)
        {
            return invoices.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
