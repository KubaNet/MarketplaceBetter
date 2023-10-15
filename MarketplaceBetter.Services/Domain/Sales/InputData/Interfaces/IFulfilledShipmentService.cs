using MarketplaceBetter.Domain.Model.Sales.InputData;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces
{
    public interface IFulfilledShipmentService
	{
		int CountForListRequest(ListRequest request);

		IList<FulfilledShipmentModel> GetForListRequest(ListRequest request);

		void AddFromFile(MemoryStream file);
	}
}
