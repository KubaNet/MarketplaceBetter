using MarketplaceBetter.Domain.Model.Amazon.Catalog;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Catalog.Interfaces
{
    public interface IAmazonChildService
    {
        AmazonChildModel Get(long id);

        IList<AmazonChildModel> GetAll();

        IList<AmazonChildModel> GetAllForParent(long parentId);

        int CountForListRequest(ListRequest request);

        IList<AmazonChildModel> GetForListRequest(ListRequest request);

        void Add(AmazonChildModel child);

        void AddForParent(long parentId);

        void AddForProductVariant(long productVariantId);

        void Update(AmazonChildModel child);

        string GetSkuFor(long? parentId, long? productVariantId);
    }
}
