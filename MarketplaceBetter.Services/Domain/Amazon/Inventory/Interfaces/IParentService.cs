using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces
{
    public interface IParentService
    {
        ParentModel Get(long id);

        IList<ParentModel> GetAll();

        IList<ParentModel> GetAllForBrand(long brandId, long instanceId);

        int CountForListRequest(ListRequest request);

        IList<ParentModel> GetForListRequest(ListRequest request);

        void Add(ParentModel parent);

        void AddForProduct(long parentId);

        void Update(ParentModel parent);

        string GetSkuFor(long? parentId, long? instanceId);
    }
}
