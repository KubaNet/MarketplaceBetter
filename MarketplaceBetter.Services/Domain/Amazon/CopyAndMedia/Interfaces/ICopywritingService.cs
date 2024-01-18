using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces
{
    public interface ICopywritingService
    {
        CopywritingModel Get(long id);

        CopywritingModel GetForProduct(long productId, long instanceId, CopywritingElementEnum element);

        int CountForListRequest(ListRequest request);

        IList<CopywritingModel> GetForListRequest(ListRequest request);

        void Add(CopywritingModel copywriting);

        void Update(CopywritingModel copywriting);

        Stream GetMissing();
    }
}
