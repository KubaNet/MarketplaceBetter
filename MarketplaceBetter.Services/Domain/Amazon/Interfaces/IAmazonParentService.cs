using MarketplaceBetter.Domain.Model.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IAmazonParentService
    {
        AmazonParentModel Get(long id);

        IList<AmazonParentModel> GetAll();

        IList<AmazonParentModel> GetAllForBrandAndInstance(long brandId, long instanceId);

        void Add(AmazonParentModel parent);

        void Update(AmazonParentModel parent);
    }
}
