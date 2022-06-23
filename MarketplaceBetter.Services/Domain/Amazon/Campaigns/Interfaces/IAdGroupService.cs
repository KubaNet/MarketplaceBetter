using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces
{
    public interface IAdGroupService
    {
        AdGroupModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<AdGroupModel> GetForListRequest(ListRequest request);

        void AddForCampaign(long campaignId);

        void Update(AdGroupModel adGroup);
    }
}
