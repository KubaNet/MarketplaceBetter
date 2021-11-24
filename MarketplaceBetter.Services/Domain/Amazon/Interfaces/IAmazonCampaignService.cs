using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IAmazonCampaignService
    {
        AmazonCampaignModel Get(long id);

        IList<AmazonCampaignModel> GetAll();

        IList<AmazonCampaignModel> GetForInstance(long instanceId);

        int CountForListRequest(ListRequest request);

        IList<AmazonCampaignModel> GetForListRequest(ListRequest request);

        void Add(AmazonCampaignModel campaign);

        void Update(AmazonCampaignModel campaign);
    }
}
