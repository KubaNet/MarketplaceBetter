using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces
{
    public interface IPortfolioService
    {
        PortfolioModel Get(long id);

        IList<PortfolioModel> GetForInstance(long instanceId);

        int CountForListRequest(ListRequest request);

        IList<PortfolioModel> GetForListRequest(ListRequest request);

        void Add(PortfolioModel portfolio);

        void Update(PortfolioModel portfolio);
    }
}
