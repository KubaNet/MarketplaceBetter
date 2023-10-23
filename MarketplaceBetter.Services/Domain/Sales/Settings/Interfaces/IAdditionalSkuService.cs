using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Settings.Interfaces
{
	public interface IAdditionalSkuService
	{
		AdditionalSkuModel Get(long id);

		int CountForListRequest(ListRequest request);

		IList<AdditionalSkuModel> GetForListRequest(ListRequest request);

		void Add(AdditionalSkuModel sku);

		void Update(AdditionalSkuModel sku);
	}
}
