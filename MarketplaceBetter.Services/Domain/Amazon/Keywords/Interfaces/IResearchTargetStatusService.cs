using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces
{
    public interface IResearchTargetStatusService
    {
        ResearchTargetStatusModel GetBySystemName(ResearchTargetStatusEnum systemName);
    }
}
