using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Services.Model;
using System.Collections.Generic;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface IProductVariantService
    {
        ProductVariantModel Get(long id);

        IList<ProductVariantModel> GetAll();

        IList<ProductVariantModel> GetAllForProduct(long productId);

        int CountForListRequest(ListRequest request);

        IList<ProductVariantModel> GetForListRequest(ListRequest request);

        void Add(ProductVariantModel productVariant);

        void Update(ProductVariantModel productVariant);
    }
}
