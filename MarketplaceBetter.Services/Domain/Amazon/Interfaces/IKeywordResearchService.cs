using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IKeywordResearchService
    {
        KeywordResearchModel Get(long id);

        IList<KeywordResearchModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<KeywordResearchModel> GetForListRequest(ListRequest request);

        void Add(KeywordResearchModel parent);

        void Update(KeywordResearchModel parent);
    }
}
