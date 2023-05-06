using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces
{
	public interface IAPlusSectionValueService
	{
		APlusSectionValueModel Get(long id);

		int CountForListRequest(ListRequest request);

		IList<APlusSectionValueModel> GetForListRequest(ListRequest request);

		int GetNextOrder(long contentId);

		void Add(APlusSectionValueModel section);

		void Update(APlusSectionValueModel section, IList<string> imagesToDelete);

		void Delete(long sectionId);
	}
}
