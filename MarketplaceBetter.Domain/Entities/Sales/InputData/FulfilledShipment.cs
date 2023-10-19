using MarketplaceBetter.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.InputData
{
    public class FulfilledShipment : Entity
    {
        public string AmazonOrderId { get; set; }

        public string ShipmentItemId { get; set; }

        public string AmazonOrderItemId { get; set; }

        public DateTime PaymentsDate { get; set; }

        public string MerchantSku { get; set; }

        public string Title { get; set; }

        public int DispatchedQuantity { get; set; }

        public long CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

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

        public long DeliveryCountryId { get; set; }

        public virtual Country DeliveryCountry { get; set; }

        public string FC { get; set; }

        public string Error { get; set; }
    }
}
