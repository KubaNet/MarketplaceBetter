using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces
{
    public interface IResearchResultService
    {
        int CountForListRequest(ListRequest request);

        IList<ResearchResultModel> GetForListRequest(ListRequest request);
    }
}
