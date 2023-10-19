using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Entities.Sales.Invoicing;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.Invoicing;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
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
        private readonly IVatRuleService _vatRuleService;
        private readonly IVariantService _variantService;

        public InvoiceService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IFulfillmentCenterService fulfillmentCenterService,
            IVatRuleService vatRuleService,
            IVariantService variantService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Invoice>();
            _fulfilledShipmentRepository = unitOfWork.GetRepository<FulfilledShipment>();
            _fulfillmentCenterService = fulfillmentCenterService;
            _vatRuleService = vatRuleService;
            _variantService = variantService;
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

            foreach (var groupedShipment in groupedShipments)
            {
                FulfilledShipmentModel firstShipment = groupedShipment.Value.First();

                CountryModel countryFrom = _fulfillmentCenterService.GetFor(firstShipment.FC).Country;
                CountryModel countryTo = firstShipment.DeliveryCountry;

                VatRuleModel vatRule = _vatRuleService.GetFor(countryFrom.Id, countryTo.Id);
                if (vatRule == null)
                {
                    foreach (var shipment in groupedShipment.Value)
                    {
                        LogErrorFor(shipment, $"No VAT rule defined for CountryFrom: {countryFrom.Name} ({countryFrom.Code}) and CountryTo: {countryTo.Name} {countryTo.Code}");
                    }

                    continue;
                }

                IList<string> invalidVariants = new List<string>();
                foreach (var shipment in groupedShipment.Value)
                {
                    VariantModel variant = _variantService.GetBySku(shipment.MerchantSku);

                    if (variant == null)
                    {
                        invalidVariants.Add(shipment.MerchantSku);
                    }
                }

                if (invalidVariants.Any())
                {
                    foreach (var shipment in groupedShipment.Value)
                    {
                        LogErrorFor(shipment, $"Unrecognized SKU: {string.Join(", ", invalidVariants)}");
                    }
                    continue;
                }

                Invoice invoice = new Invoice();

                invoice.OrderId = firstShipment.AmazonOrderId;
                invoice.PaymentDate = firstShipment.PaymentsDate;
                invoice.Buyer = $"{firstShipment.RecipientName}\r\n{firstShipment.DeliveryAddress1} {firstShipment.DeliveryAddress2} {firstShipment.DeliveryAddress3}\r\n{firstShipment.DeliveryPostcode} {firstShipment.DeliveryCityTown} {firstShipment.DeliveryCounty}\r\n{firstShipment.DeliveryCountry.Name}";
                invoice.BuyerFirstName = GetFirstName(firstShipment.RecipientName);
                invoice.BuyerLastName = GetLastName(firstShipment.RecipientName);
                invoice.BuyerStreet = $"{firstShipment.DeliveryAddress1} {firstShipment.DeliveryAddress2} {firstShipment.DeliveryAddress3}";
                invoice.BuyerCity = firstShipment.DeliveryCityTown;
                invoice.BuyerPostalCode = firstShipment.DeliveryPostcode;
                invoice.BuyerState = firstShipment.DeliveryCounty;
                invoice.BuyerCountry = firstShipment.DeliveryCountry.Name;
                invoice.ShippingGrossPrice = groupedShipment.Value.Sum(s => s.DeliveryPrice + s.DeliveryTax + s.ShipmentPromoDiscount);
                if (groupedShipment.Value.Sum(s => s.ShipmentPromoDiscount) != 0)
                {
                    invoice.ShippingGrossPrice -= groupedShipment.Value.Sum(s => s.DeliveryTax);
                }
                invoice.VatRuleId = vatRule.Id;

                invoice.Entries = new List<InvoiceEntry>();

                foreach (var shipment in groupedShipment.Value)
                {
                    InvoiceEntry entry = new InvoiceEntry();

                    entry.OrderItemId = shipment.AmazonOrderItemId;
                    entry.ShipmentItemId = shipment.ShipmentItemId;
                    entry.CurrencyId = shipment.Currency.Id;
                    entry.ProductName = shipment.Title;
                    entry.GrossPrice = shipment.ItemPrice + shipment.ItemTax + shipment.GiftWrapPrice + shipment.GiftWrappingTax + shipment.ItemPromoDiscount;
                    entry.Quantity = shipment.DispatchedQuantity;
                    entry.Sku = shipment.MerchantSku;
                    entry.VariantId = _variantService.GetBySku(shipment.MerchantSku).Id;

                    invoice.Entries.Add(entry);
                }

                _repository.Add(invoice);
                _unitOfWork.Save();
            }
        }

        private string GetFirstName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return "Amazon";
            }

            string[] fullNameParts = fullName.Split(' ');

            if (fullNameParts.Length < 1 || string.IsNullOrWhiteSpace(fullNameParts[0]))
            {
                return "Amazon";
            }

            return fullNameParts[0];
        }

        private string GetLastName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return "Customer";
            }

            string[] fullNameParts = fullName.Split(' ');
            string lastName = string.Empty;

            if (fullNameParts.Length < 2)
            {
                return "Customer";
            }

            for (int i = 1; i < fullNameParts.Length; i++)
            {
                lastName += fullNameParts[i];

                if (i < fullNameParts.Length - 1)
                {
                    lastName += " ";
                }
            }

            return lastName;
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

                    continue;
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
