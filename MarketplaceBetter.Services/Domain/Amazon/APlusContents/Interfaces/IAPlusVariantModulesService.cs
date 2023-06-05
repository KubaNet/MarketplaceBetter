using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces
{
    public interface IAPlusVariantModulesService
    {
        int CountForListRequest(ListRequest request);

        IList<APlusVariantModulesModel> GetForListRequest(ListRequest request);
    }
}
