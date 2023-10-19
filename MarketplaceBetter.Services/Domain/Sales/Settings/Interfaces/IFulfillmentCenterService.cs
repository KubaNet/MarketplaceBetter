using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Settings.Interfaces
{
    public interface IFulfillmentCenterService
    {
        FulfillmentCenterModel Get(long id);

        FulfillmentCenterModel GetFor(string code);

        int CountForListRequest(ListRequest request);

        IList<FulfillmentCenterModel> GetForListRequest(ListRequest request);

        void Add(FulfillmentCenterModel center);

        void Update(FulfillmentCenterModel center);
    }
}
