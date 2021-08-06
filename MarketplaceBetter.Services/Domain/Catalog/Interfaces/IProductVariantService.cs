using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Services.Model.ListRequests.Catalog;
using System.Collections.Generic;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface IProductVariantService
    {
        ProductVariantModel Get(long id);

        IList<ProductVariantModel> GetAll();

        IList<ProductVariantModel> GetForProduct(long productId);

        int CountForListRequest(ProductVariantListRequest request);

        IList<ProductVariantModel> GetForListRequest(ProductVariantListRequest request);

        void Add(ProductVariantModel productVariant);

        void Update(ProductVariantModel productVariant);
    }
}
