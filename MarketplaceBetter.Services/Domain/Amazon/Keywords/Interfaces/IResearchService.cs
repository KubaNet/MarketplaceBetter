using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces
{
    public interface IResearchService
    {
        ResearchModel Get(long id);

        IList<ResearchModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<ResearchModel> GetForListRequest(ListRequest request);

        void Add(ResearchModel research);

        void Update(ResearchModel research);
    }
}
