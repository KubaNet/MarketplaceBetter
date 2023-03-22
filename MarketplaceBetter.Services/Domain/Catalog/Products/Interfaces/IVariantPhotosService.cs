using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces
{
    public interface IVariantPhotosService
    {
        int CountForListRequest(ListRequest request);

        IList<VariantPhotosModel> GetForListRequest(ListRequest request);
    }
}
