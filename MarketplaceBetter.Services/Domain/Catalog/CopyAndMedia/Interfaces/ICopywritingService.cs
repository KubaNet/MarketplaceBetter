using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces
{
    public interface ICopywritingService
    {
        CopywritingModel Get(long id);

        CopywritingModel GetForProduct(long productId, long instanceId, CopywritingElementEnum element);

        int CountForListRequest(ListRequest request);

        IList<CopywritingModel> GetForListRequest(ListRequest request);

        void Add(CopywritingModel copywriting);

        void Update(CopywritingModel copywriting);
    }
}
