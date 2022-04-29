using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces
{
    public interface ICopywritingService
    {
        int CountForListRequest(ListRequest request);

        IList<CopywritingModel> GetForListRequest(ListRequest request);

        void Add(CopywritingModel copywriting);

        void Update(CopywritingModel copywriting);
    }
}
