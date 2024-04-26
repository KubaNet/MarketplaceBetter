using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.InputData
{
    public class CustomerReturn : Entity
    {
        public DateTime ReturnDate { get; set; }

        public string OrderId { get; set; }

        public string Sku { get; set; }

        public string Asin { get; set; }

        public string Fnsku { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public string FulfillmentCenterId { get; set; }

        public long DetailedDispositionId { get; set; }

        public virtual ReturnDetailedDisposition DetailedDisposition { get; set; }

        public long ReasonId { get; set; }

        public virtual ReturnReason Reason { get; set; }

        public long StatusId { get; set; }

        public virtual ReturnStatus Status { get; set; }

        public string LicensePlateNumber { get; set; }

        public string CustomerComments { get;set;}

    }
}
