using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces
{
    public interface IResearchTargetService
    {
        ResearchTargetModel Get(long id);

        IList<ResearchTargetModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<ResearchTargetModel> GetForListRequest(ListRequest request);

        void Add(ResearchTargetModel target);

        void Update(ResearchTargetModel target);
    }
}
