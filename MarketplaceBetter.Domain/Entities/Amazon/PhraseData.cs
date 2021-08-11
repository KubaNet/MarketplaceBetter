using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon
{
    public class PhraseData : Entity
    {
        public string Phrase { get; set; }

        public double Value { get; set; }

        public bool IsIncluded { get; set; }

        public long KeywordDataId { get; set; }

        public virtual KeywordData KeywordData { get; set; }
    }
}
