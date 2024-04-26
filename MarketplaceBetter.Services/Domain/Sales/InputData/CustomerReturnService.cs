using CsvHelper.Configuration;
using CsvHelper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using MarketplaceBetter.Infrastructure.Data;

namespace MarketplaceBetter.Services.Domain.Sales.InputData
{
    public class CustomerReturnService : ICustomerReturnService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CustomerReturn> _repository;
        private readonly IReturnDetailedDispositionService _returnDetailedDispositionService;
        private readonly IReturnReasonService _returnReasonService;
        private readonly IReturnStatusService _returnStatusService;

        public CustomerReturnService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IReturnDetailedDispositionService returnDetailedDispositionService,
            IReturnReasonService returnReasonService,
            IReturnStatusService returnStatusService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<CustomerReturn>();
            _returnDetailedDispositionService = returnDetailedDispositionService;
            _returnReasonService = returnReasonService;
            _returnStatusService = returnStatusService;
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<CustomerReturn> returns = _repository.GetQuery();

            returns = ApplyFilter(returns, request);

            return returns.Count();
        }

        public IList<CustomerReturnModel> GetForListRequest(ListRequest request)
        {
            IQueryable<CustomerReturn> returns = _repository.GetQuery();

            returns = ApplyFilter(returns, request);
            returns = ApplySorting(returns, request);
            returns = ApplyPaging(returns, request);

            return _mapper.Map<IList<CustomerReturnModel>>(returns);
        }

        public void AddFromFile(MemoryStream file)
        {
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
            };

            using var reader = new StreamReader(file);
            using var csv = new CsvReader(reader, config);

            csv.Read();
            csv.ReadHeader();

            IList<CustomerReturn> returns = new List<CustomerReturn>();

            while (csv.Read())
            {
                CustomerReturn customerReturn = new CustomerReturn();

                //customerReturn.AmazonOrderId = csv.GetField("Amazon Order Id");
                //customerReturn.ShipmentItemId = csv.GetField("customerReturn Item ID");

                //if (_repository.Any(r => r.AmazonOrderId == customerReturn.AmazonOrderId && s.ShipmentItemId == customerReturn.ShipmentItemId))
                //{
                //    continue;
                //}

                customerReturn.ReturnDate = csv.GetField<DateTime>("return-date");
                customerReturn.OrderId = csv.GetField("order-id");
                customerReturn.Sku = csv.GetField("sku");
                customerReturn.Asin = csv.GetField("asin");
                customerReturn.Fnsku = csv.GetField("fnsku");
                customerReturn.ProductName = csv.GetField("product-name");
                customerReturn.Quantity = csv.GetField<int>("quantity");
                customerReturn.FulfillmentCenterId = csv.GetField("fulfillment-center-id");
                customerReturn.DetailedDisposition = _returnDetailedDispositionService.GetByName(csv.GetField("detailed-disposition"));
                customerReturn.Reason = _returnReasonService.GetByName(csv.GetField("reason"));
                customerReturn.Status = _returnStatusService.GetByName(csv.GetField("status"));
                customerReturn.LicensePlateNumber = csv.GetField("license-plate-number");
                customerReturn.CustomerComments = csv.GetField("customer-comments");

                _repository.Add(customerReturn);
                returns.Add(customerReturn);
            }

            _unitOfWork.Save();

            //_invoiceService.Create(_mapper.Map<IList<CustomerReturnModel>>(returns));
        }

        private IQueryable<CustomerReturn> ApplyFilter(IQueryable<CustomerReturn> returns, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return returns;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "order_id", "sku", "asin", "fnsku", "product_name", "fulfillment_center_id",
                    "detailed_disposition", "reason", "status", "license_plate_number", "customer_comments" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    returns = searchField.Name switch
                    {
                        "id" => returns.Where(r => r.Id == searchField.Value.ParseToIntOrDefault()),
                        "order_id" => returns.Where(r => r.OrderId.Contains(searchField.Value)),
                        "sku" => returns.Where(r => r.Sku.Contains(searchField.Value)),
                        "asin" => returns.Where(r => r.Asin.Contains(searchField.Value)),
                        "fnsku" => returns.Where(r => r.Fnsku.Contains(searchField.Value)),
                        "product_name" => returns.Where(r => r.ProductName.Contains(searchField.Value)),
                        "fulfillment_center_id" => returns.Where(r => r.FulfillmentCenterId.Contains(searchField.Value)),
                        "detailed_disposition" => returns.Where(r => r.DetailedDisposition.Name.Contains(searchField.Value)),
                        "reason" => returns.Where(r => r.Reason.Name.Contains(searchField.Value)),
                        "status" => returns.Where(r => r.Status.Name.Contains(searchField.Value)),
                        "license_plate_number" => returns.Where(r => r.LicensePlateNumber.Contains(searchField.Value)),
                        "customer_comments" => returns.Where(r => r.CustomerComments.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    returns = returns.Where(r => r.Id == searchString.ParseToIntOrDefault()
                        || r.OrderId.Contains(searchString)
                        || r.Sku.Contains(searchString)
                        || r.Asin.Contains(searchString)
                        || r.Fnsku.Contains(searchString)
                        || r.ProductName.Contains(searchString)
                        || r.FulfillmentCenterId.Contains(searchString)
                        || r.DetailedDisposition.Name.Contains(searchString)
                        || r.Reason.Name.Contains(searchString)
                        || r.Status.Name.Contains(searchString)
                        || r.LicensePlateNumber.Contains(searchString)
                        || r.CustomerComments.Contains(searchString)
                        );
                }
            }

            return returns;
        }

        private IQueryable<CustomerReturn> ApplySorting(IQueryable<CustomerReturn> returns, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                returns = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.Id) : returns.OrderByDescending(r => r.Id),
                    "order_id" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.OrderId) : returns.OrderByDescending(r => r.OrderId),
                    "sku" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.Sku) : returns.OrderByDescending(r => r.Sku),
                    "asin" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.Asin) : returns.OrderByDescending(r => r.Asin),
                    "fnsku" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.Fnsku) : returns.OrderByDescending(r => r.Fnsku),
                    "product_name" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.ProductName) : returns.OrderByDescending(r => r.ProductName),
                    "fulfillment_center_id" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.FulfillmentCenterId) : returns.OrderByDescending(r => r.FulfillmentCenterId),
                    "detailed_disposition" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.DetailedDisposition) : returns.OrderByDescending(r => r.DetailedDisposition),
                    "reason" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.Reason) : returns.OrderByDescending(r => r.Reason),
                    "status" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.Status) : returns.OrderByDescending(r => r.Status),
                    "license_plate_number" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.LicensePlateNumber) : returns.OrderByDescending(r => r.LicensePlateNumber),
                    "customer_comments" => request.SortDirection == SortDirection.Ascending ? returns.OrderBy(r => r.CustomerComments) : returns.OrderByDescending(r => r.CustomerComments),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                returns = returns.OrderBy(r => r.Id);
            }

            return returns;
        }

        private IQueryable<CustomerReturn> ApplyPaging(IQueryable<CustomerReturn> returns, ListRequest request)
        {
            return returns.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
