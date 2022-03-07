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
    public interface IAmazonTargetingService
    {
        AmazonTargetingModel Get(long id);

        IList<AmazonTargetingModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<AmazonTargetingModel> GetForListRequest(ListRequest request);

        void Add(AmazonTargetingModel Targeting, Stream file);

        void SetAsNegative(AmazonTargetingModel targeting);

        void SetAsNegative(IList<AmazonTargetingModel> targeting);

        void SetAsActive(AmazonTargetingModel targeting);

        void SetAsActive(IList<AmazonTargetingModel> targeting);

        void SetAsKeyword(AmazonTargetingModel targeting);

        void SetAsProduct(AmazonTargetingModel targeting);

        Stream ExportTargeting(ListRequest request);
    }
}
