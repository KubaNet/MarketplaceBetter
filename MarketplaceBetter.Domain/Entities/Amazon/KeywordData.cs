using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class KeywordData : Entity
    {
        public long SourceId { get; set; }

        public virtual KeywordDataSource Source { get; set; }

        public long MainPhraseId { get; set; }

        public virtual MainPhrase MainPhrase { get; set; }
    }
}
