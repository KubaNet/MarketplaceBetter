using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IAmazonTargetingStatusService
    {
        AmazonTargetingStatusModel GetBySystemName(AmazonTargetingStatusEnum systemName);
    }
}
