using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

        void AddFromFile(MemoryStream file, AddResearchTargetsModel model);

        void Update(ResearchTargetModel target);

        void SetAsIncluded(ResearchTargetModel target);

        void SetAsIncluded(IList<ResearchTargetModel> targets);

        void SetAsExcluded(ResearchTargetModel target);

        void SetAsExcluded(IList<ResearchTargetModel> targets);
    }
}
