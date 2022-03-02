using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IKeywordResearchTargetService
    {
        KeywordResearchTargetModel Get(long id);

        IList<KeywordResearchTargetModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<KeywordResearchTargetModel> GetForListRequest(ListRequest request);

        void Add(KeywordResearchTargetModel target);

        void Update(KeywordResearchTargetModel target);
    }
}
