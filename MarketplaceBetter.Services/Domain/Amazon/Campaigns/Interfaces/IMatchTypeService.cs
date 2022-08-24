using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces
{
    public interface IMatchTypeService
    {
        IList<MatchTypeModel> GetAll();

        IList<MatchTypeModel> GetAllNegative();
    }
}
