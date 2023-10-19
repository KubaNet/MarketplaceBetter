using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Entities.Sales.Invoicing;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Sales.Invocing.Interfaces;
using MarketplaceBetter.Services.Domain.Sales.Settings.Interfaces;
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
        private readonly IRepository<FulfilledShipment> _fulfilledShipmentRepository;
        private readonly IFulfillmentCenterService _fulfillmentCenterService;

        public InvoiceService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IFulfillmentCenterService fulfillmentCenterService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Invoice>();
            _fulfilledShipmentRepository = unitOfWork.GetRepository<FulfilledShipment>();
            _fulfillmentCenterService = fulfillmentCenterService;
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

        public void Create(IList<FulfilledShipmentModel> shipments)
        {
            IDictionary<string, IList<FulfilledShipmentModel>> groupedShipments = new Dictionary<string, IList<FulfilledShipmentModel>>();

            groupedShipments = GroupShipments(shipments);
        }

        private IDictionary<string, IList<FulfilledShipmentModel>> GroupShipments(IList<FulfilledShipmentModel> shipments)
        {
            IDictionary<string, IList<FulfilledShipmentModel>> groupedShipments = new Dictionary<string, IList<FulfilledShipmentModel>>();

            foreach (var shipment in shipments)
            {
                FulfillmentCenterModel fulfillmentCenter = _fulfillmentCenterService.GetFor(shipment.FC);

                if (fulfillmentCenter == null)
                {
                    LogErrorFor(shipment, $"Unrecognized Fulfillement Center: {shipment.FC}");
                }

                string key = $"{shipment.AmazonOrderId}_{fulfillmentCenter.Country.Code}";
                if (!groupedShipments.ContainsKey(key))
                {
                    groupedShipments.Add(key, new List<FulfilledShipmentModel>());
                }

                groupedShipments[key].Add(shipment);
            }

            return groupedShipments;
        }

        private void LogErrorFor(FulfilledShipmentModel errorShipment, string error)
        {
            FulfilledShipment shipment = _fulfilledShipmentRepository.Get(errorShipment.Id);

            shipment.Error = error;

            _fulfilledShipmentRepository.Update(shipment);
            _unitOfWork.Save();
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
