using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces
{
    public interface IFulfilledShipmentService
    {
        int CountForListRequest(ListRequest request);

        IList<FulfilledShipmentModel> GetForListRequest(ListRequest request);

        void AddFromFile(MemoryStream file);

        Stream GetSalesReport(DateTime dateFrom, DateTime dateTo);
    }
}
