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
    public interface IChildService
    {
        ChildModel Get(long id);

        IList<ChildModel> GetAll();

        IList<ChildModel> GetAllForBrand(long brandId, long instanceId);

        int CountForListRequest(ListRequest request);

        IList<ChildModel> GetForListRequest(ListRequest request);

        void Add(ChildModel child);

        void AddForVariant(long variantId);

        void Update(ChildModel child);

        void Delete(long id);

        void DeleteAllForVariant(long variantId);

        string GetSkuFor(long? variantId, long? instanceId);
    }
}
