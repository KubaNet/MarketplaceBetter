using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces
{
    public interface ICampaignService
    {
        CampaignModel Get(long id);

        IList<CampaignModel> GetAll();

        IList<CampaignModel> GetForInstance(long instanceId);

        int CountForListRequest(ListRequest request);

        IList<CampaignModel> GetForListRequest(ListRequest request);

        void Add(CampaignModel campaign);

        void Update(CampaignModel campaign);
    }
}
