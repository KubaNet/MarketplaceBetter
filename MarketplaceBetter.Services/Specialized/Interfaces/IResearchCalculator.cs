using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface IResearchCalculator
    {
        IList<ResearchResult> Calculate(Research research, IList<ResearchTarget> targets);
    }
}
