using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.OperationsModel.Amazon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Operations.Amazon.Interfaces
{
    public interface IKeywordResearchTargetOperationsService
    {
        AddKeywordResearchTargetsFromAdvertisingReportModel CreateTargetsFromAdvertisingReport(KeywordResearchModel research, Stream file);
    }
}
