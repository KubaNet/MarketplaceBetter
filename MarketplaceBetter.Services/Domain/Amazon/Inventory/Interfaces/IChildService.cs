using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
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

        IList<ChildModel> GetAllForParent(long parentId);

        int CountForListRequest(ListRequest request);

        IList<ChildModel> GetForListRequest(ListRequest request);

        void Add(ChildModel child);

        void AddForParent(long parentId);

        void AddForVariant(long VariantId);

        void Update(ChildModel child);

        string GetSkuFor(long? VariantId);
    }
}
