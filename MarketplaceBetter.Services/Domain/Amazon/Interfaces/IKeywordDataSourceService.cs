using MarketplaceBetter.Domain.Model.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IKeywordDataSourceService
    {
        KeywordDataSourceModel Get(long id);

        IList<KeywordDataSourceModel> GetAll();
    }
}
