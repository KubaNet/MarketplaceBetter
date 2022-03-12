using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces
{
    public interface IAmazonParentInstanceService
    {
        AmazonParentInstanceModel Get(long id);

        IList<AmazonParentInstanceModel> GetAll();

        IList<AmazonParentInstanceModel> GetAllForBrand(long brandId, long instanceId);

        int CountForListRequest(ListRequest request);

        IList<AmazonParentInstanceModel> GetForListRequest(ListRequest request);

        void Add(AmazonParentInstanceModel parent);

        void AddForParent(long parentId);

        void Update(AmazonParentInstanceModel parent);

        string GetSkuFor(long? parentId, long? instanceId);
    }
}
