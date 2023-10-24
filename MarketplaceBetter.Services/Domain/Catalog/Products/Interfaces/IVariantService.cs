using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Services.Model;
using System.Collections.Generic;

namespace MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces
{
    public interface IVariantService
    {
        VariantModel Get(long id);

        VariantModel GetBySkuOrAdditionalSku(string sku);

        IList<VariantModel> GetAll();

        IList<VariantModel> GetAllForProduct(long productId);

        int CountForListRequest(ListRequest request);

        IList<VariantModel> GetForListRequest(ListRequest request);

        void Add(VariantModel variant);

        void Update(VariantModel variant);

        void Delete(long id);
    }
}
