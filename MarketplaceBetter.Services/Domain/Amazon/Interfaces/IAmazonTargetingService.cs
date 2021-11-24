using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IAmazonTargetingService
    {
        AmazonTargetingModel Get(long id);

        IList<AmazonTargetingModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<AmazonTargetingModel> GetForListRequest(ListRequest request);

        void Add(AmazonTargetingModel Targeting);

        void Update(AmazonTargetingModel Targeting);
    }
}
