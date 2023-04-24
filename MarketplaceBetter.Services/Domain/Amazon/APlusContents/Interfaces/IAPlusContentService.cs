using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces
{
    public interface IAPlusContentService
    {
        APlusContentModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<APlusContentModel> GetForListRequest(ListRequest request);

        void Add(APlusContentModel content);

        void Update(APlusContentModel content);
    }
}
