using AutoMapper;
using CsvHelper.Configuration;
using CsvHelper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace MarketplaceBetter.Services.Domain.Sales.Settings
{
	public class ProductAccountingDataService : IProductAccountingDataService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<ProductAccountingData> _repository;
		private readonly IRepository<Child> _childRepository;
		private readonly IRepository<Currency> _currencyRepository;
		private readonly IRepository<AdditionalSku> _additionalSkuRepository;

		public ProductAccountingDataService(
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_repository = unitOfWork.GetRepository<ProductAccountingData>();
			_childRepository = unitOfWork.GetRepository<Child>();
			_currencyRepository = unitOfWork.GetRepository<Currency>();
			_additionalSkuRepository = unitOfWork.GetRepository<AdditionalSku>();
		}

		public ProductAccountingDataModel Get(long id) => _mapper.Map<ProductAccountingDataModel>(_repository.Get(id));

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

		public void Add(ProductAccountingDataModel data)
		{
			ProductAccountingData dataToAdd = new();

			TransferValues(dataToAdd, data);

			_repository.Add(dataToAdd);
			_unitOfWork.Save();
		}

		public void Update(ProductAccountingDataModel data)
		{
			ProductAccountingData dataToUpdate = _repository.Get(data.Id);

			TransferValues(dataToUpdate, data);

			_repository.Update(dataToUpdate);
			_unitOfWork.Save();
		}

		public Stream Export()
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter = ";",
				Encoding = Encoding.UTF8,
				HasHeaderRecord = false,
			};

			CsvWriter csv = new CsvWriter(writer, config);

			WriteHeader(csv);

			IList<ProductAccountingData> productsData = _repository.GetAll();

			foreach (var productData in productsData)
			{
				WriteProductData(productData, csv);
			}

			writer.Flush();
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}

		private void WriteProductData(ProductAccountingData productData, CsvWriter csv)
		{
			IList<Child> childs = _childRepository.Where(c => c.Variant.ProductId == productData.ProductId).ToList();
			foreach (var child in childs)
			{
				foreach (var currency in _currencyRepository.GetAll())
				{
					ProductionCost cost = productData.ProductionCosts.SingleOrDefault(c => c.CurrencyId == currency.Id);

					csv.WriteField(child.Sku);
					csv.WriteField(productData.InvoiceName);
					csv.WriteField(productData.CommodityCode);
					csv.WriteField(productData.Weight);
					csv.WriteField(cost?.Cost);
					csv.WriteField(cost?.Currency.Name);

					csv.NextRecord();
				}
			}

			IList<AdditionalSku> additionalSkus = _additionalSkuRepository.Where(s => s.Variant.ProductId == productData.ProductId).ToList();
			foreach (var additionalSku in additionalSkus)
			{
				foreach (var currency in _currencyRepository.GetAll())
				{
					ProductionCost cost = productData.ProductionCosts.SingleOrDefault(c => c.CurrencyId == currency.Id);

					csv.WriteField(additionalSku.Sku);
					csv.WriteField(productData.InvoiceName);
					csv.WriteField(productData.CommodityCode);
					csv.WriteField(productData.Weight);
					csv.WriteField(cost?.Cost);
					csv.WriteField(cost?.Currency.Name);

					csv.NextRecord();
				}
			}
		}

		private void WriteHeader(CsvWriter csv)
		{
			csv.WriteField("SKU");
			csv.WriteField("Invoice Name");
			csv.WriteField("Commodity Code");
			csv.WriteField("Weight");
			csv.WriteField("Production Cost Value");
			csv.WriteField("Production Cost Currency");
			csv.NextRecord();
		}

		private void TransferValues(ProductAccountingData toData, ProductAccountingDataModel fromData)
		{
			toData.ProductId = fromData.Product.Id;
			toData.InvoiceName = fromData.InvoiceName;
			toData.CommodityCode = fromData.CommodityCode;
			toData.Weight = fromData.Weight;

			foreach (var fromCost in fromData.ProductionCosts)
			{
				if (toData.Id == 0)
				{
					ProductionCost toCost = new ProductionCost();

					TransferValues(toCost, fromCost);

					toData.ProductionCosts.Add(toCost);
				}
				else
				{
					ProductionCost toCost = toData.ProductionCosts.Single(c => c.Id == fromCost.Id);

					TransferValues(toCost, fromCost);
				}
			}
		}

		private void TransferValues(ProductionCost toCost, ProductionCostModel fromCost)
		{
			toCost.Cost = fromCost.Cost;
			toCost.CurrencyId = fromCost.Currency.Id;
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
					"weight" => request.SortDirection == SortDirection.Ascending ? data.OrderBy(d => d.Weight) : data.OrderByDescending(d => d.Weight),
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
