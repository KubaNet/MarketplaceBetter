using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces
{
    public interface IKeywordTargetService
    {
        KeywordTargetModel Get(long id);

        IList<KeywordTargetModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<KeywordTargetModel> GetForListRequest(ListRequest request);

        void Add(KeywordTargetModel target);

        void Update(KeywordTargetModel target);
    }
}
