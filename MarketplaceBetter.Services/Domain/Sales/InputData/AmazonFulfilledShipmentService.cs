using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.InputData
{
	public class AmazonFulfilledShipmentService : IAmazonFulfilledShipmentService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<AmazonFulfilledShipment> _repository;

        public AmazonFulfilledShipmentService(
			IMapper mapper,
			IUnitOfWork unitOfWork)
        {
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_repository = unitOfWork.GetRepository<AmazonFulfilledShipment>();
        }

        public int CountForListRequest(ListRequest request)
		{
			IQueryable<AmazonFulfilledShipment> shipments = _repository.GetQuery();

			shipments = ApplyFilter(shipments, request);

			return shipments.Count();
		}

		public IList<AmazonFulfilledShipmentModel> GetForListRequest(ListRequest request)
		{
			IQueryable<AmazonFulfilledShipment> shipments = _repository.GetQuery();

			shipments = ApplyFilter(shipments, request);
			shipments = ApplySorting(shipments, request);
			shipments = ApplyPaging(shipments, request);

			return _mapper.Map<IList<AmazonFulfilledShipmentModel>>(shipments);
		}

		private IQueryable<AmazonFulfilledShipment> ApplyFilter(IQueryable<AmazonFulfilledShipment> shipments, ListRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.SearchString))
			{
				return shipments;
			}

			IList<string> searchStrings = request.SearchString.SplitForFiltering();

			foreach (string searchString in searchStrings)
			{
				string[] searchFieldNames = new[] { "id", "amazon_order_id", "amazon_order_item_id", "merchant_sku", "dispatched_quantity", "currency", "item_price", "item_tax", 
					"delivery_price", "delivery_tax", "gif_wrap_price", "gift_wrapping_tax", "item_promo_discount", "shipment_promo_discount" };
				SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

				if (searchField != null)
				{
					shipments = searchField.Name switch
					{
						"id" => shipments.Where(s => s.Id == searchField.Value.ParseToIntOrDefault()),
						"amazon_order_id" => shipments.Where(s => s.AmazonOrderId.Contains(searchField.Value)),
						"amazon_order_item_id" => shipments.Where(s => s.AmazonOrderItemId.Contains(searchField.Value)),
						"merchant_sku" => shipments.Where(s => s.MerchantSku.Contains(searchField.Value)),
						"dispatched_quantity" => shipments.Where(s => s.DispatchedQuantity == searchField.Value.ParseToIntOrDefault()),
						"currency" => shipments.Where(s => s.Currency.Name.Contains(searchField.Value)),
						"item_price" => shipments.Where(s => s.ItemPrice == searchField.Value.ParseToDecimalOrDefault()),
						"item_tax" => shipments.Where(s => s.ItemTax == searchField.Value.ParseToDecimalOrDefault()),
						"delivery_price" => shipments.Where(s => s.DeliveryPrice == searchField.Value.ParseToDecimalOrDefault()),
						"delivery_tax" => shipments.Where(s => s.DeliveryTax == searchField.Value.ParseToDecimalOrDefault()),
						"gif_wrap_price" => shipments.Where(s => s.GiftWrapPrice == searchField.Value.ParseToDecimalOrDefault()),
						"gift_wrapping_tax" => shipments.Where(s => s.GiftWrappingTax == searchField.Value.ParseToDecimalOrDefault()),
						"item_promo_discount" => shipments.Where(s => s.ItemPromoDiscount == searchField.Value.ParseToDecimalOrDefault()),
						"shipment_promo_discount" => shipments.Where(s => s.ShipmentPromoDiscount == searchField.Value.ParseToDecimalOrDefault()),
						_ => throw new UnrecognizedSearchFieldException(searchField.Name)
					};
				}
				else
				{
					shipments = shipments.Where(s => s.Id == searchString.ParseToIntOrDefault()
						|| s.AmazonOrderId.Contains(searchString)
						|| s.AmazonOrderItemId.Contains(searchString)
						|| s.MerchantSku.Contains(searchString)
						|| s.DispatchedQuantity == searchString.ParseToIntOrDefault()
						|| s.Currency.Name.Contains(searchString)
						|| s.ItemPrice == searchString.ParseToDecimalOrDefault()
						|| s.ItemTax == searchString.ParseToDecimalOrDefault()
						|| s.DeliveryPrice == searchString.ParseToDecimalOrDefault()
						|| s.DeliveryTax == searchString.ParseToDecimalOrDefault()
						|| s.GiftWrapPrice == searchString.ParseToDecimalOrDefault()
						|| s.GiftWrappingTax == searchString.ParseToDecimalOrDefault()
						|| s.ItemPromoDiscount == searchString.ParseToDecimalOrDefault()
						|| s.ShipmentPromoDiscount == searchString.ParseToDecimalOrDefault());
				}
			}

			return shipments;
		}

		private IQueryable<AmazonFulfilledShipment> ApplySorting(IQueryable<AmazonFulfilledShipment> shipments, ListRequest request)
		{
			if (!string.IsNullOrWhiteSpace(request.SortBy))
			{
				shipments = request.SortBy switch
				{
					"id" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.Id) : shipments.OrderByDescending(s => s.Id),
					"amazon_order_id" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.AmazonOrderId) : shipments.OrderByDescending(s => s.AmazonOrderId),
					"amazon_order_item_id" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.AmazonOrderItemId) : shipments.OrderByDescending(s => s.AmazonOrderItemId),
					"merchant_sku" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.MerchantSku) : shipments.OrderByDescending(s => s.MerchantSku),
					"dispatched_quantity" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DispatchedQuantity) : shipments.OrderByDescending(s => s.DispatchedQuantity),
					"currency" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.Currency.Name) : shipments.OrderByDescending(s => s.Currency.Name),
					_ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
				};
			}
			else
			{
				shipments = shipments.OrderBy(s => s.Id);
			}

			return shipments;
		}

		private IQueryable<AmazonFulfilledShipment> ApplyPaging(IQueryable<AmazonFulfilledShipment> shipments, ListRequest request)
		{
			return shipments.Skip(request.Page * request.PageSize).Take(request.PageSize);
		}
	}
}
