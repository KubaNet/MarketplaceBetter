using MarketplaceBetter.Domain.Model.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IKeywordDataService
    {
        KeywordDataModel Get(long id);

        IList<KeywordDataModel> GetAll();

        void Add(KeywordDataModel data);

        void Update(KeywordDataModel data);
    }
}
