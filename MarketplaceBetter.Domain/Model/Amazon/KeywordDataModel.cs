using MarketplaceBetter.Domain.Entities.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class KeywordDataModel
    {
        public long Id { get; set; }

        public MainPhraseModel MainPhrase { get; set; }

        public KeywordDataSourceModel Source { get; set; }
    }
}
