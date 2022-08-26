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
    public interface INegativeProductService
    {
        NegativeProductModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<NegativeProductModel> GetForListRequest(ListRequest request);

        void Add(NegativeProductModel negativeProduct, CampaignModel campaign);

        void Update(NegativeProductModel negativeProduct);

        void UpdateStatus(long negativeProductId, AdEntityStatusEnum status);
    }
}
