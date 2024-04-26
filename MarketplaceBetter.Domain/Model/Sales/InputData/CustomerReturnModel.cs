using MarketplaceBetter.Domain.Entities.Sales.InputData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.InputData
{
    public class CustomerReturnModel
    {
        public long Id { get; set; }

        public DateTime ReturnDate { get; set; }

        public string OrderId { get; set; }

        public string Sku { get; set; }

        public string Asin { get; set; }

        public string Fnsku { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public string FulfillmentCenterId { get; set; }

        public ReturnDetailedDispositionModel DetailedDisposition { get; set; }

        public ReturnReasonModel Reason { get; set; }

        public ReturnStatusModel Status { get; set; }

        public string LicensePlateNumber { get; set; }

        public string CustomerComments { get; set; }
    }
}
