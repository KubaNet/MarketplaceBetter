using MarketplaceBetter.Domain.Model.Advertising.Campaigns;
using MarketplaceBetter.Services.Model;
using System.Collections.Generic;

namespace MarketplaceBetter.Services.Domain.Advertising.Campaigns.Interfaces
{
    public interface IPortfolioService
    {
        PortfolioModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<PortfolioModel> GetForListRequest(ListRequest request);

        void Update(PortfolioModel portfolio);
    }
}
