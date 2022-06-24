using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces
{
    public interface IProductAdService
    {
        ProductAdModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<ProductAdModel> GetForListRequest(ListRequest request);

        void AddForAdGroup(long adGroupId, long productId);

        void Update(ProductAdModel productAd);
    }
}
