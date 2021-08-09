using MarketplaceBetter.Domain.Model.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IKeywordAnalysisService
    {
        KeywordAnalysisModel Get(long id);

        IList<KeywordAnalysisModel> GetAll();

        void Add(KeywordAnalysisModel analysis);

        void Update(KeywordAnalysisModel analysis);
    }
}
