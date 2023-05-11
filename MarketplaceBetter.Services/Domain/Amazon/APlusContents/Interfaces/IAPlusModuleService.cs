using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces
{
    public interface IAPlusModuleService
    {
        IList<APlusModuleModel> GetAll();
    }
}
