using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces
{
    public interface IAmazonParentService
    {
        AmazonParentModel Get(long id);

        IList<AmazonParentModel> GetAll();

        IList<AmazonParentModel> GetAllForBrand(long brandId);

        int CountForListRequest(ListRequest request);

        IList<AmazonParentModel> GetForListRequest(ListRequest request);

        void Add(AmazonParentModel parent);

        void Update(AmazonParentModel parent);
    }
}
