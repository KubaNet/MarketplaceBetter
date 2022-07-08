using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Campaigns
{
    public enum MatchTypeEnum
    {
        None = 0,

        Exact = 1,

        Broad = 2,

        Phrase = 3,

        NegativeExact = 4,

        NegativePhrase = 5
    }
}
