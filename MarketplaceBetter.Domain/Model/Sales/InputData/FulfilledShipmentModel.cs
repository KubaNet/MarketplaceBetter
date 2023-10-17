using MarketplaceBetter.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.InputData
{
    public class FulfilledShipmentModel
    {
        public long Id { get; set; }

        public string AmazonOrderId { get; set; }

        public string ShipmentItemId { get; set; }

        public string AmazonOrderItemId { get; set; }

        public string MerchantSku { get; set; }

        public int DispatchedQuantity { get; set; }

        public CurrencyModel Currency { get; set; }

        public decimal ItemPrice { get; set; }

        public decimal ItemTax { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal DeliveryTax { get; set; }

        public decimal GiftWrapPrice { get; set; }

        public decimal GiftWrappingTax { get; set; }

        public decimal ItemPromoDiscount { get; set; }

        public decimal ShipmentPromoDiscount { get; set; }

        public string RecipientName { get; set; }

        public string DeliveryAddress1 { get; set; }

        public string DeliveryAddress2 { get; set; }

        public string DeliveryAddress3 { get; set; }

        public string DeliveryCityTown { get; set; }

        public string DeliveryCounty { get; set; }

        public string DeliveryPostcode { get; set; }

        public CountryModel DeliveryCountry { get; set; }

        public string FC { get; set; }
    }
}
