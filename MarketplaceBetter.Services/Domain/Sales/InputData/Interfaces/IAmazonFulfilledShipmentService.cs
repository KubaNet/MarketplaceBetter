using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces
{
	public interface IAmazonFulfilledShipmentService
	{
		int CountForListRequest(ListRequest request);

		IList<AmazonFulfilledShipmentModel> GetForListRequest(ListRequest request);

		void AddFromFile(MemoryStream file);
	}
}
