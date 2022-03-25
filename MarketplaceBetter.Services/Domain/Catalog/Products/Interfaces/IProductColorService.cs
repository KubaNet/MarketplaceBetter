using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces
{
    public interface IProductColorService
    {
        int CountForListRequest(ListRequest request);

        IList<ProductColorModel> GetForListRequest(ListRequest request);
    }
}
