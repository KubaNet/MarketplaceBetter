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
    public interface INegativeKeywordTargetingService
    {
        NegativeKeywordTargetingModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<NegativeKeywordTargetingModel> GetForListRequest(ListRequest request);

        void Update(NegativeKeywordTargetingModel negativeKeywordTargeting);

        void UpdateStatus(long negativeKeywordTargetingId, AdEntityStatusEnum status);
    }
}
