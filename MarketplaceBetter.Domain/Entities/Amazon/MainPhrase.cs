using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class MainPhrase : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long StatusId { get; set; }

        public virtual MainPhraseStatus Status { get; set; }

        public long AnalysisId { get; set; }

        public virtual KeywordAnalysis Analysis { get; set; }
    }
}
