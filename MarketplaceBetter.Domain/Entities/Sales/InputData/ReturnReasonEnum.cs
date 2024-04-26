using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.InputData
{
    public enum ReturnReasonEnum
    {
        None = 0,

        OTHER = 1,

        ORDERED_WRONG_ITEM = 2,

        FOUND_BETTER_PRICE = 3,

        NO_REASON_GIVEN = 4,

        QUALITY_UNACCEPTABLE = 5,

        NOT_COMPATIBLE = 6,

        DAMAGED_BY_FC = 7,

        MISSED_ESTIMATED_DELIVERY = 8,

        MISSING_PARTS = 9,

        DAMAGED_BY_CARRIER = 10,

        SWITCHEROO = 11,

        DEFECTIVE = 12,

        EXTRA_ITEM = 13,

        UNWANTED_ITEM = 14,

        WARRANTY = 15,

        UNAUTHORISED_PURCHASE = 16,

        UNDELIVERABLE_INSUFFICIENT_ADDRESS = 17,

        UNDELIVERABLE_FAILED_DELIVERY_ATTEMPTS = 18,

        UNDELIVERABLE_REFUSED = 19,

        UNDELIVERABLE_UNKNOWN = 20,

        UNDELIVERABLE_UNCLAIMED = 21,

        CLOTHING_TOO_SMALL = 22,

        CLOTHING_TOO_LARGE = 23,

        CLOTHING_STYLE = 24,

        MISORDERED = 25,

        NOT_AS_DESCRIBED = 26,

        JEWELLERY_TOO_SMALL = 27,

        JEWELLERY_TOO_LARGE = 28,

        JEWELLERY_BATTERY = 29,

        JEWELLERY_NO_DOCS = 30,

        JEWELLERY_BAD_CLASP = 31,

        JEWELLERY_LOOSE_STONE = 32,

        JEWELLERY_NO_CERT = 33,

        APPAREL_TOO_SMALL = 34,

        APPAREL_TOO_LARGE = 35,

        APPAREL_STYLE = 36,
    }
}
