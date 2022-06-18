using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Keywords
{
    public class ResearchResult : Entity
    {
        public string Phrase { get; set; }

        public int Score { get; set; }

        public int WordsCount { get; set; }

        public int Frequency { get; set; }

        public bool IsOriginal { get; set; }

        public long? TargetId { get; set; }

        public virtual ResearchTarget Target { get; set; }

        public long ResearchId { get; set; }

        public virtual Research Research { get; set; }

        public override string ToString()
        {
            return Phrase;
        }
    }
}
