using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces
{
    public interface IResearchTargetOperationsService
    {
        AddAdvertisingReportResearchTargetsModel CreateTargetsFromAdvertisingReport(ResearchModel research, Stream file);
    }
}
