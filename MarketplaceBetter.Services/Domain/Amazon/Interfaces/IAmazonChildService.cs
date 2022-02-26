using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IAmazonChildService
    {
        AmazonChildModel Get(long id);

        IList<AmazonChildModel> GetAll();

        IList<AmazonChildModel> GetAllForParent(long parentId);

        int CountForListRequest(ListRequest request);

        IList<AmazonChildModel> GetForListRequest(ListRequest request);

        void Add(AmazonChildModel child);

        void Update(AmazonChildModel child);

        string GetAsinForProductVariant(ProductVariantModel productVariant);
    }
}
