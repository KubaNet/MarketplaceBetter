using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Keywords
{
    public class ResearchResultModel
    {
        public long Id { get; set; }

        public string Phrase { get; set; }

        public int Score { get; set; }

        public int WordsCount { get; set; }

        public int Frequency { get; set; }

        public bool IsOriginal { get; set; }

        public ResearchTargetModel Target { get; set; }

        public ResearchModel Research { get; set; }

        public override string ToString()
        {
            return Phrase;
        }
    }
}
