using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Entities.Sales.Invoicing;
using MarketplaceBetter.Domain.Entities.Sales.Settings;
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
using MarketplaceBetter.Services.Specialized.Interfaces;
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
        private readonly IRepository<ProductAccountingData> _productAccountingData;
        private readonly IFulfillmentCenterService _fulfillmentCenterService;
        private readonly IVatRuleService _vatRuleService;
        private readonly IVariantService _variantService;
        private readonly IFakturowoService _fakturowoService;

        public InvoiceService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IFulfillmentCenterService fulfillmentCenterService,
            IVatRuleService vatRuleService,
            IVariantService variantService,
            IFakturowoService fakturowoService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Invoice>();
            _fulfilledShipmentRepository = unitOfWork.GetRepository<FulfilledShipment>();
            _productAccountingData = unitOfWork.GetRepository<ProductAccountingData>();
            _fulfillmentCenterService = fulfillmentCenterService;
            _vatRuleService = vatRuleService;
            _variantService = variantService;
            _fakturowoService = fakturowoService;
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
            IDictionary<string, IList<FulfilledShipmentModel>> groupedShipments = FilterAndGroupShipments(shipments);

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
                        LogErrorFor(shipment, $"No VAT rule defined for CountryFrom: {countryFrom.Name} ({countryFrom.Code}) and CountryTo: {countryTo.Name} ({countryTo.Code})");
                    }

                    continue;
                }

                IList<string> invalidVariants = new List<string>();
                IList<ProductModel> productsWithMissingAccountingData = new List<ProductModel>();
                foreach (var shipment in groupedShipment.Value)
                {
                    VariantModel variant = _variantService.GetBySkuOrAdditionalSku(shipment.MerchantSku);

                    if (variant == null)
                    {
                        invalidVariants.Add(shipment.MerchantSku);
                    }
                    else
                    {
                        ProductAccountingData productData = _productAccountingData.SingleOrDefault(d => d.ProductId == variant.Product.Id);

                        if (productData == null)
                        {
                            productsWithMissingAccountingData.Add(variant.Product);

                            continue;
                        }
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

                if (productsWithMissingAccountingData.Any())
                {
                    foreach (var shipment in groupedShipment.Value)
                    {
                        LogErrorFor(shipment, $"Missing Accounting Data for products: {string.Join(", ", productsWithMissingAccountingData.Select(p => p.Name))}");
                    }

                    continue;
                }

                Invoice invoice = new Invoice();

                invoice.OrderId = firstShipment.AmazonOrderId;
                invoice.PaymentDate = firstShipment.PaymentsDate;
                invoice.Buyer = ClearProblematicChars($"{firstShipment.RecipientName}\r\n{firstShipment.DeliveryAddress1} {firstShipment.DeliveryAddress2} {firstShipment.DeliveryAddress3}\r\n{firstShipment.DeliveryPostcode} {firstShipment.DeliveryCityTown} {firstShipment.DeliveryCounty}\r\n{firstShipment.DeliveryCountry.Name}");
                invoice.BuyerFirstName = ClearProblematicChars(GetFirstName(firstShipment.RecipientName));
                invoice.BuyerLastName = ClearProblematicChars(GetLastName(firstShipment.RecipientName));
                invoice.BuyerStreet = ClearProblematicChars(GetStreet($"{firstShipment.DeliveryAddress1} {firstShipment.DeliveryAddress2} {firstShipment.DeliveryAddress3}"));
                invoice.BuyerCity = ClearProblematicChars(firstShipment.DeliveryCityTown);
                invoice.BuyerPostalCode = ClearProblematicChars(firstShipment.DeliveryPostcode);
                invoice.BuyerState = ClearProblematicChars(firstShipment.DeliveryCounty);
                invoice.BuyerCountry = ClearProblematicChars(firstShipment.DeliveryCountry.Name);
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

                    VariantModel variant = _variantService.GetBySkuOrAdditionalSku(shipment.MerchantSku);

                    entry.OrderItemId = shipment.AmazonOrderItemId;
                    entry.ShipmentItemId = shipment.ShipmentItemId;
                    entry.InvoiceName = _productAccountingData.Single(d => d.ProductId == variant.Product.Id).InvoiceName;
                    entry.VariantId = variant.Id;
                    entry.CurrencyId = shipment.Currency.Id;
                    entry.GrossPrice = shipment.ItemPrice + shipment.ItemTax + shipment.GiftWrapPrice + shipment.GiftWrappingTax + shipment.ItemPromoDiscount;
                    entry.Quantity = shipment.DispatchedQuantity;

                    invoice.Entries.Add(entry);
                }

                _repository.Add(invoice);
                _unitOfWork.Save();

                foreach (var shipment in groupedShipment.Value)
                {
                    FulfilledShipment shipmentToUpdate = _fulfilledShipmentRepository.Get(shipment.Id);

                    shipmentToUpdate.InvoiceId = invoice.Id;

                    _fulfilledShipmentRepository.Update(shipmentToUpdate);
                    _unitOfWork.Save();
                }
            }
        }

        public async Task<int> Issue(InvoiceModel invoice, int nextNumber)
        {
            if (invoice.IsIssued)
            {
                return nextNumber;
            }

            if (invoice.VatRule.CountryFrom.Code.Equals("ES", StringComparison.InvariantCultureIgnoreCase))
            {
                invoice.Number = $"{nextNumber}/PL/{invoice.VatRule.CountryTo.Code}/{DateTime.Now.Year}";
            }
            else
            {
                invoice.Number = $"{nextNumber}/{invoice.VatRule.CountryFrom.Code}/{invoice.VatRule.CountryTo.Code}/{DateTime.Now.Year}";
            }

            await _fakturowoService.Issue(invoice);

            Invoice invoiceToUpdate = _repository.Get(invoice.Id);

            TransferIssueValues(invoiceToUpdate, invoice);

            _repository.Update(invoiceToUpdate);
            _unitOfWork.Save();

            return invoice.IsIssued ? ++nextNumber : nextNumber;
        }

        private void TransferIssueValues(Invoice toInvoice, InvoiceModel fromInvoice)
        {
            toInvoice.IsIssued = fromInvoice.IsIssued;
            toInvoice.ApiNumber = fromInvoice.ApiNumber;
            toInvoice.ApiError = fromInvoice.ApiError;
            if (fromInvoice.IsIssued)
            {
                toInvoice.Number = fromInvoice.Number;
            }
            else
            {
                fromInvoice.Number = null;
            }
        }

        private string ClearProblematicChars(string value)
        {
            return value.Replace("&", null);
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

        private string GetStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                return "Amazon Customer Street";
            }

            return street;
        }

        private IDictionary<string, IList<FulfilledShipmentModel>> FilterAndGroupShipments(IList<FulfilledShipmentModel> shipments)
        {
            IDictionary<string, IList<FulfilledShipmentModel>> groupedShipments = new Dictionary<string, IList<FulfilledShipmentModel>>();

            foreach (var shipment in shipments)
            {
                if (shipment.Invoice != null)
                {
                    continue;
                }

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
                string[] searchFieldNames = new[] { "id", "number", "api_number", "api_error", "order_id", "buyer_first_name", "buyer_last_name",
                    "buyer_street", "buyer_city", "buyer_postal_code", "buyer_state", "buyer_country", "vat_number", "vat_value" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    invoices = searchField.Name switch
                    {
                        "id" => invoices.Where(i => i.Id == searchField.Value.ParseToIntOrDefault()),
                        "number" => invoices.Where(i => i.Number.Contains(searchField.Value)),
                        "api_number" => invoices.Where(i => i.ApiNumber.Contains(searchField.Value)),
                        "api_error" => invoices.Where(i => i.ApiError.Contains(searchField.Value)),
                        "order_id" => invoices.Where(i => i.OrderId.Contains(searchField.Value)),
                        "buyer_first_name" => invoices.Where(i => i.BuyerFirstName.Contains(searchField.Value)),
                        "buyer_last_name" => invoices.Where(i => i.BuyerLastName.Contains(searchField.Value)),
                        "buyer_street" => invoices.Where(i => i.BuyerStreet.Contains(searchField.Value)),
                        "buyer_city" => invoices.Where(i => i.BuyerCity.Contains(searchField.Value)),
                        "buyer_postal_code" => invoices.Where(i => i.BuyerPostalCode.Contains(searchField.Value)),
                        "buyer_state" => invoices.Where(i => i.BuyerState.Contains(searchField.Value)),
                        "buyer_country" => invoices.Where(i => i.BuyerCountry.Contains(searchField.Value)),
                        "vat_number" => invoices.Where(i => i.VatRule.VatNumber.Contains(searchField.Value)),
                        "vat_value" => invoices.Where(i => i.VatRule.VatValue == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    invoices = invoices.Where(i => i.Id == searchString.ParseToIntOrDefault()
                        || i.Number.Contains(searchString)
                        || i.ApiNumber.Contains(searchString)
                        || i.ApiError.Contains(searchString)
                        || i.OrderId.Contains(searchString)
                        || i.BuyerFirstName.Contains(searchString)
                        || i.BuyerLastName.Contains(searchString)
                        || i.BuyerStreet.Contains(searchString)
                        || i.BuyerCity.Contains(searchString)
                        || i.BuyerPostalCode.Contains(searchString)
                        || i.BuyerState.Contains(searchString)
                        || i.BuyerCountry.Contains(searchString)
                        || i.VatRule.VatNumber.Contains(searchString)
                        || i.VatRule.VatValue == searchString.ParseToIntOrDefault());
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
                    "is_issued" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.IsIssued) : invoices.OrderByDescending(i => i.IsIssued),
                    "api_number" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.ApiNumber) : invoices.OrderByDescending(i => i.ApiNumber),
                    "api_error" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.ApiError) : invoices.OrderByDescending(i => i.ApiError),
                    "order_id" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.OrderId) : invoices.OrderByDescending(i => i.OrderId),
                    "buyer_first_name" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.BuyerFirstName) : invoices.OrderByDescending(i => i.BuyerFirstName),
                    "buyer_last_name" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.BuyerLastName) : invoices.OrderByDescending(i => i.BuyerLastName),
                    "buyer_street" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.BuyerStreet) : invoices.OrderByDescending(i => i.BuyerStreet),
                    "buyer_city" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.BuyerCity) : invoices.OrderByDescending(i => i.BuyerCity),
                    "buyer_postal_code" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.BuyerPostalCode) : invoices.OrderByDescending(i => i.BuyerPostalCode),
                    "buyer_state" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.BuyerState) : invoices.OrderByDescending(i => i.BuyerState),
                    "buyer_country" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.BuyerCountry) : invoices.OrderByDescending(i => i.BuyerCountry),
                    "vat_number" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.VatRule.VatNumber) : invoices.OrderByDescending(i => i.VatRule.VatNumber),
                    "vat_value" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.VatRule.VatValue) : invoices.OrderByDescending(i => i.VatRule.VatValue),
                    "payment_date" => request.SortDirection == SortDirection.Ascending ? invoices.OrderBy(i => i.PaymentDate) : invoices.OrderByDescending(i => i.PaymentDate),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                invoices = invoices.OrderByDescending(i => i.Id);
            }

            return invoices;
        }

        private IQueryable<Invoice> ApplyPaging(IQueryable<Invoice> invoices, ListRequest request)
        {
            return invoices.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
