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
					"delivery_price", "delivery_tax", "gif_wrap_price", "gift_wrapping_tax", "item_promo_discount", "shipment_promo_discount", "recipient_name", "delivery_address_1",
                    "delivery_address_2", "delivery_address_3", "delivery_city_town", "delivery_county", "delivery_postcode", "delivery_country", "fc" };
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
						"recipient_name" => shipments.Where(s => s.RecipientName.Contains(searchField.Value)),
						"delivery_address_1" => shipments.Where(s => s.DeliveryAddress1.Contains(searchField.Value)),
                        "delivery_address_2" => shipments.Where(s => s.DeliveryAddress2.Contains(searchField.Value)),
                        "delivery_address_3" => shipments.Where(s => s.DeliveryAddress3.Contains(searchField.Value)),
                        "delivery_city_town" => shipments.Where(s => s.DeliveryCityTown.Contains(searchField.Value)),
                        "delivery_county" => shipments.Where(s => s.DeliveryCounty.Contains(searchField.Value)),
                        "delivery_postcode" => shipments.Where(s => s.DeliveryPostcode.Contains(searchField.Value)),
                        "delivery_country" => shipments.Where(s => s.DeliveryCountry.Name.Contains(searchField.Value)),
                        "fc" => shipments.Where(s => s.FC.Contains(searchField.Value)),
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
						|| s.ShipmentPromoDiscount == searchString.ParseToDecimalOrDefault()
						|| s.RecipientName.Contains(searchString)
						|| s.DeliveryAddress1.Contains(searchString)
                        || s.DeliveryAddress2.Contains(searchString)
                        || s.DeliveryAddress3.Contains(searchString)
                        || s.DeliveryCityTown.Contains(searchString)
                        || s.DeliveryCounty.Contains(searchString)
                        || s.DeliveryPostcode.Contains(searchString)
                        || s.DeliveryCountry.Name.Contains(searchString)
                        || s.FC.Contains(searchString));
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
                    "item_price" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.ItemPrice) : shipments.OrderByDescending(s => s.ItemPrice),
                    "item_tax" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.ItemTax) : shipments.OrderByDescending(s => s.ItemTax),
                    "delivery_price" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryPrice) : shipments.OrderByDescending(s => s.DeliveryPrice),
                    "deliver_tax" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryTax) : shipments.OrderByDescending(s => s.DeliveryTax),
                    "gift_wrap_price" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.GiftWrapPrice) : shipments.OrderByDescending(s => s.GiftWrapPrice),
                    "gift_wrapping_tax" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.GiftWrappingTax) : shipments.OrderByDescending(s => s.GiftWrappingTax),
                    "item_promo_discount" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.ItemPromoDiscount) : shipments.OrderByDescending(s => s.ItemPromoDiscount),
                    "shipment_promo_discount" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.ShipmentPromoDiscount) : shipments.OrderByDescending(s => s.ShipmentPromoDiscount),
                    "recipient_name" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.RecipientName) : shipments.OrderByDescending(s => s.RecipientName),
                    "delivery_address_1" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryAddress1) : shipments.OrderByDescending(s => s.DeliveryAddress1),
                    "delivery_address_2" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryAddress2) : shipments.OrderByDescending(s => s.DeliveryAddress2),
                    "delivery_address_3" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryAddress3) : shipments.OrderByDescending(s => s.DeliveryAddress3),
                    "delivery_city_town" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryCityTown) : shipments.OrderByDescending(s => s.DeliveryCityTown),
                    "delivery_county" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryCounty) : shipments.OrderByDescending(s => s.DeliveryCounty),
                    "delivery_postcode" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryPostcode) : shipments.OrderByDescending(s => s.DeliveryPostcode),
                    "delivery_country" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.DeliveryCountry.Name) : shipments.OrderByDescending(s => s.DeliveryCountry.Name),
                    "fc" => request.SortDirection == SortDirection.Ascending ? shipments.OrderBy(s => s.FC) : shipments.OrderByDescending(s => s.FC),
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
