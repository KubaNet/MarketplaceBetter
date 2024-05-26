using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Entities.Sales.Invoicing;
using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Sales.Invoicing.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Invoicing
{
    public class CorrectiveInvoiceService : ICorrectiveInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CorrectiveInvoice> _repository;
        private readonly IRepository<Invoice> _invoiceRepository;

        public CorrectiveInvoiceService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<CorrectiveInvoice>();
            _invoiceRepository = unitOfWork.GetRepository<Invoice>();
        }
        public int CountForListRequest(ListRequest request)
        {
            IQueryable<CorrectiveInvoice> correctiveInvoices = _repository.GetQuery();

            correctiveInvoices = ApplyFilter(correctiveInvoices, request);

            return correctiveInvoices.Count();
        }

        public IList<CorrectiveInvoiceModel> GetForListRequest(ListRequest request)
        {
            IQueryable<CorrectiveInvoice> correctiveInvoices = _repository.GetQuery();

            correctiveInvoices = ApplyFilter(correctiveInvoices, request);
            correctiveInvoices = ApplySorting(correctiveInvoices, request);
            correctiveInvoices = ApplyPaging(correctiveInvoices, request);

            return _mapper.Map<IList<CorrectiveInvoiceModel>>(correctiveInvoices);
        }

        public void Create(IList<CustomerReturnModel> returns)
        {
            IDictionary<string, IList<CustomerReturnModel>> groupedReturns = FilterAndGroupReturns(returns);

            foreach (var groupedReturn in groupedReturns)
            {
                IList<Invoice> invoices = _invoiceRepository.Where(i => i.OrderId.Equals(groupedReturn.Key)).ToList();
                IList<CorrectiveInvoice> correctiveInvoices = _repository.Where(ci => invoices.Any(i => i.Id == ci.InvoiceId)).ToList();

                IList<CustomerReturnModel> returnsToCorrect = groupedReturn.Value;
                foreach (var invoice in invoices)
                {
                    if (!CanBeCorrected(invoice, correctiveInvoices, returnsToCorrect))
                    {
                        break;
                    }

                    if (!returnsToCorrect.Any())
                    {
                        break;
                    }    
                }
            }
        }

        public Task<int> Issue(CorrectiveInvoiceModel correctiveInvoice, int nextNumber)
        {
            throw new NotImplementedException();
        }

        private IQueryable<CorrectiveInvoice> ApplyFilter(IQueryable<CorrectiveInvoice> correctiveInvoices, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return correctiveInvoices;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "number", "api_number", "api_error" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    correctiveInvoices = searchField.Name switch
                    {
                        "id" => correctiveInvoices.Where(i => i.Id == searchField.Value.ParseToIntOrDefault()),
                        "number" => correctiveInvoices.Where(i => i.Number.Contains(searchField.Value)),
                        "api_number" => correctiveInvoices.Where(i => i.ApiNumber.Contains(searchField.Value)),
                        "api_error" => correctiveInvoices.Where(i => i.ApiError.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    correctiveInvoices = correctiveInvoices.Where(i => i.Id == searchString.ParseToIntOrDefault()
                        || i.Number.Contains(searchString)
                        || i.ApiNumber.Contains(searchString)
                        || i.ApiError.Contains(searchString));
                }
            }

            return correctiveInvoices;
        }

        private IQueryable<CorrectiveInvoice> ApplySorting(IQueryable<CorrectiveInvoice> correctiveInvoices, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                correctiveInvoices = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? correctiveInvoices.OrderBy(i => i.Id) : correctiveInvoices.OrderByDescending(i => i.Id),
                    "number" => request.SortDirection == SortDirection.Ascending ? correctiveInvoices.OrderBy(i => i.Number) : correctiveInvoices.OrderByDescending(i => i.Number),
                    "is_issued" => request.SortDirection == SortDirection.Ascending ? correctiveInvoices.OrderBy(i => i.IsIssued) : correctiveInvoices.OrderByDescending(i => i.IsIssued),
                    "api_number" => request.SortDirection == SortDirection.Ascending ? correctiveInvoices.OrderBy(i => i.ApiNumber) : correctiveInvoices.OrderByDescending(i => i.ApiNumber),
                    "api_error" => request.SortDirection == SortDirection.Ascending ? correctiveInvoices.OrderBy(i => i.ApiError) : correctiveInvoices.OrderByDescending(i => i.ApiError),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                correctiveInvoices = correctiveInvoices.OrderByDescending(i => i.Id);
            }

            return correctiveInvoices;
        }

        private IQueryable<CorrectiveInvoice> ApplyPaging(IQueryable<CorrectiveInvoice> correctiveInvoices, ListRequest request)
        {
            return correctiveInvoices.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }

        private IDictionary<string, IList<CustomerReturnModel>> FilterAndGroupReturns(IList<CustomerReturnModel> returns)
        {
            IDictionary<string, IList<CustomerReturnModel>> groupedReturns = new Dictionary<string, IList<CustomerReturnModel>>();

            foreach (var singleReturn in returns)
            {
                if (singleReturn.CorrectiveInvoice != null)
                {
                    continue;
                }

                string key = singleReturn.OrderId;
                if (!groupedReturns.ContainsKey(key))
                {
                    groupedReturns.Add(key, new List<CustomerReturnModel>());
                }

                groupedReturns[key].Add(singleReturn);
            }

            return groupedReturns;
        }

        private bool CanBeCorrected(Invoice invoice, IList<CorrectiveInvoice> correctiveInvoices, IList<CustomerReturnModel> returnsToCorrect)
        {
            if (correctiveInvoices.Any())
            {
                CorrectiveInvoice lastCorrectiveInvoice = correctiveInvoices.OrderByDescending(i => i.IssueDate).First();

                foreach (var returnToCorrect in returnsToCorrect)
                {
                    if (lastCorrectiveInvoice.Entries.Any(e =>
                        e.InvoiceEntry.Variant.Asin.Equals(returnToCorrect.Asin) && e.Quantity > 0))
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (var returnToCorrect in returnsToCorrect)
                {
                    if (invoice.Entries.Any(e =>
                        e.Variant.Asin.Equals(returnToCorrect.Asin)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
