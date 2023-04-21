using MarketplaceBetter.Domain.Model.Catalog.Attributes;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Attributes.Interfaces
{
    public interface IProductDimensionsService
    {
        ProductDimensionsModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<ProductDimensionsModel> GetForListRequest(ListRequest request);

        void Add(ProductDimensionsModel dimensions);

        void Update(ProductDimensionsModel dimensions);
    }
}
