using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces
{
    public interface IChildInstanceService
    {
        ChildInstanceModel Get(long id);

        IList<ChildInstanceModel> GetAll();

        IList<ChildInstanceModel> GetAllForBrand(long brandId, long instanceId);

        int CountForListRequest(ListRequest request);

        IList<ChildInstanceModel> GetForListRequest(ListRequest request);

        void Add(ChildInstanceModel child);

        void AddForChild(long childId);

        void Update(ChildInstanceModel child);

        void Delete(long id);

        void DeleteAllForChild(long childId);

        void ChangeStatus(IList<ChildInstanceModel> childInstances, EntityStatusModel status);

        string GetSkuFor(long? childId, long? instanceId);
    }
}
