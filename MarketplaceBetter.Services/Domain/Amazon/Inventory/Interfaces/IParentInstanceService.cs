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
    public interface IParentInstanceService
    {
        ParentInstanceModel Get(long id);

        IList<ParentInstanceModel> GetAll();

        IList<ParentInstanceModel> GetAllForBrand(long brandId, long instanceId);

        int CountForListRequest(ListRequest request);

        IList<ParentInstanceModel> GetForListRequest(ListRequest request);

        void Add(ParentInstanceModel parent);

        void AddForProduct(long parentId);

        void Update(ParentInstanceModel parent);

        string GetSkuFor(long? parentId, long? instanceId);
    }
}
