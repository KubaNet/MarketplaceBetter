using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces
{
	public interface IAPlusModuleValueService
	{
		APlusModuleValueModel Get(long id);

		int CountForListRequest(ListRequest request);

		IList<APlusModuleValueModel> GetForListRequest(ListRequest request);

        IList<APlusModuleValueModel> GetAllChartsForProduct(long productId, int order);

        int GetNextOrder(long contentId);

		void Add(APlusModuleValueModel module);

		void Update(APlusModuleValueModel module, IList<string> imagesToDelete);

		void Delete(long id);
	}
}
