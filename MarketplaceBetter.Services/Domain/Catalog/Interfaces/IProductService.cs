using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Services.Model.ListRequests.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface IProductService
    {
        ProductModel Get(long id);

        IList<ProductModel> GetAll();

        IList<ProductModel> GetForBrand(long brandId);

        IList<ProductModel> GetForListRequest(ProductListRequest request);

        void Add(ProductModel product);

        void Update(ProductModel product);
    }
}
