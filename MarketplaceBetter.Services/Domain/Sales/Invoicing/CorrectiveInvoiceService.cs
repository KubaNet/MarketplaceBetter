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
        private readonly IRepository<CustomerReturn> _customerReturnRepository;
        private readonly IRepository<Invoice> _invoiceRepository;

        public CorrectiveInvoiceService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<CorrectiveInvoice>();
            _invoiceRepository = unitOfWork.GetRepository<Invoice>();
            _customerReturnRepository = unitOfWork.GetRepository<CustomerReturn>();
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
                IList<Invoice> invoices = _invoiceRepository.Where(i => i.OrderId.Equals(groupedReturn.Key) && i.IsIssued).ToList();
                IList<long> invoicesIds = invoices.Select(i => i.Id).ToList();
                IList<CorrectiveInvoice> correctiveInvoices = _repository.Where(ci => invoicesIds.Contains(ci.InvoiceId)).ToList();

                IList<CustomerReturnModel> returnsToCorrect = groupedReturn.Value;
                foreach (var invoice in invoices)
                {
                    IList<CorrectiveInvoice> invoiceCorrectiveInvoices = _repository.Where(ci => ci.InvoiceId == invoice.Id).ToList();

                    if (!returnsToCorrect.Any(r => r.Quantity > 0))
                    {
                        break;
                    }

                    CorrectiveInvoice lastCorrectiveInvoice = invoiceCorrectiveInvoices.OrderByDescending(i => i.IssueDate).FirstOrDefault();

                    CorrectiveInvoice correctiveInvoice = new CorrectiveInvoice();
                    if (lastCorrectiveInvoice != null)
                    {
                        GetValuesFrom(correctiveInvoice, lastCorrectiveInvoice);
                    }
                    else
                    {
                        GetValuesFrom(correctiveInvoice, invoice);
                    }

                    bool isCorrected = false;
                    foreach (var returnToCorrect in returnsToCorrect)
                    {
                        CorrectiveInvoiceEntry entry = correctiveInvoice.Entries.SingleOrDefault(e => e.InvoiceEntry.Variant.Asin.Equals(returnToCorrect.Asin) && e.Quantity > 0);

                        if (entry != null)
                        {
                            isCorrected = true;
                            int adjustedQuantityCount = 0;
                            for (int i = 0; i < returnToCorrect.Quantity; i++)
                            {
                                entry.GrossPrice -= entry.GrossPrice / entry.Quantity;
                                entry.Quantity -= 1;
                                adjustedQuantityCount++;

                                if (entry.Quantity == 0)
                                {
                                    break;
                                }
                            }

                            returnToCorrect.Quantity -= adjustedQuantityCount;
                            
                            CustomerReturn customerReturn = _customerReturnRepository.Get(returnToCorrect.Id);
                            customerReturn.CorrectiveInvoices.Add(correctiveInvoice);
                        }
                    }

                    if (isCorrected)
                    {
                        _repository.Add(correctiveInvoice);
                        _unitOfWork.Save();
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
                string[] searchFieldNames = new[] { "id", "invoice_id", "number", "invoice_number", "api_number", "api_error" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    correctiveInvoices = searchField.Name switch
                    {
                        "id" => correctiveInvoices.Where(i => i.Id == searchField.Value.ParseToIntOrDefault()),
                        "invoice_id" => correctiveInvoices.Where(i => i.Invoice.Id == searchField.Value.ParseToIntOrDefault()),
                        "number" => correctiveInvoices.Where(i => i.Number.Contains(searchField.Value)),
                        "invoice_number" => correctiveInvoices.Where(i => i.Invoice.Number.Contains(searchField.Value)),
                        "api_number" => correctiveInvoices.Where(i => i.ApiNumber.Contains(searchField.Value)),
                        "api_error" => correctiveInvoices.Where(i => i.ApiError.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    correctiveInvoices = correctiveInvoices.Where(i => i.Id == searchString.ParseToIntOrDefault()
                        || i.Invoice.Id == searchString.ParseToIntOrDefault()
                        || i.Number.Contains(searchString)
                        || i.Invoice.Number.Contains(searchString)
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
                    "invoice_id" => request.SortDirection == SortDirection.Ascending ? correctiveInvoices.OrderBy(i => i.Invoice.Id) : correctiveInvoices.OrderByDescending(i => i.Invoice.Id),
                    "number" => request.SortDirection == SortDirection.Ascending ? correctiveInvoices.OrderBy(i => i.Number) : correctiveInvoices.OrderByDescending(i => i.Number),
                    "invoice_number" => request.SortDirection == SortDirection.Ascending ? correctiveInvoices.OrderBy(i => i.Invoice.Number) : correctiveInvoices.OrderByDescending(i => i.Invoice.Number),
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
                string key = singleReturn.OrderId;
                if (!groupedReturns.ContainsKey(key))
                {
                    groupedReturns.Add(key, new List<CustomerReturnModel>());
                }

                groupedReturns[key].Add(singleReturn);
            }

            return groupedReturns;
        }

        private void GetValuesFrom(CorrectiveInvoice correctiveInvoiceTo, CorrectiveInvoice correctiveInvoiceFrom)
        {
            correctiveInvoiceTo.Invoice = correctiveInvoiceFrom.Invoice;

            foreach (var entry in correctiveInvoiceFrom.Entries)
            {
                correctiveInvoiceTo.Entries.Add(
                    new CorrectiveInvoiceEntry
                    {
                        GrossPrice = entry.GrossPrice,
                        Quantity = entry.Quantity,
                        InvoiceEntry = entry.InvoiceEntry
                    });
            }
        }

        private void GetValuesFrom(CorrectiveInvoice correctiveInvoiceTo, Invoice invoiceFrom)
        {
            correctiveInvoiceTo.Invoice = invoiceFrom;

            foreach (var entry in invoiceFrom.Entries)
            {
                correctiveInvoiceTo.Entries.Add(
                    new CorrectiveInvoiceEntry
                    {
                        GrossPrice = entry.GrossPrice,
                        Quantity = entry.Quantity,
                        InvoiceEntry = entry
                    });
            }
        }
    }
}
