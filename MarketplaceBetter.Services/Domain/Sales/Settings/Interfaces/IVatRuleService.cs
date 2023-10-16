using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Settings.Interfaces
{
	public interface IVatRuleService
	{
		VatRuleModel Get(long id);

		int CountForListRequest(ListRequest request);

		IList<VatRuleModel> GetForListRequest(ListRequest request);

		void Add(VatRuleModel rule);

		void Update(VatRuleModel rule);
	}
}
