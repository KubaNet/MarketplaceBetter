using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces
{
    public interface INegativeKeywordService
    {
        NegativeKeywordModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<NegativeKeywordModel> GetForListRequest(ListRequest request);

        void Add(NegativeKeywordModel negativeKeyword, CampaignModel campaign);

        void Update(NegativeKeywordModel negativeKeyword);

        void UpdateStatus(long negativeKeywordId, AdEntityStatusEnum status);
    }
}
