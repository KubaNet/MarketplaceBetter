using MarketplaceBetter.Domain.Entities.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class PhraseDataModel
    {
        public long Id { get; set; }

        public string Phrase { get; set; }

        public double Value { get; set; }

        public bool IsIncluded { get; set; }

        public KeywordDataModel KeywordData { get; set; }
    }
}
