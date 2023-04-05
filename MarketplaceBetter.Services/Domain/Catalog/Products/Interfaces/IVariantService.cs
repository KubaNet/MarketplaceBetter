using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Services.Model;
using System.Collections.Generic;

namespace MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces
{
    public interface IVariantService
    {
        VariantModel Get(long id);

        IList<VariantModel> GetAll();

        IList<VariantModel> GetAllForProduct(long productId);

        int CountForListRequest(ListRequest request);

        IList<VariantModel> GetForListRequest(ListRequest request);

        void Add(VariantModel Variant);

        void Update(VariantModel Variant);

        void Delete(long id);
    }
}
