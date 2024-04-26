using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Services.Model;
using System.Collections.Generic;
using System.IO;

namespace MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces
{
    public interface ICustomerReturnService
    {
        int CountForListRequest(ListRequest request);

        IList<CustomerReturnModel> GetForListRequest(ListRequest request);

        void AddFromFile(MemoryStream file);
    }
}
