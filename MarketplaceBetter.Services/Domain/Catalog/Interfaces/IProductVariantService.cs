using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface IProductVariantService
    {
        ProductVariantModel Get(long id);

        IList<ProductVariantModel> GetAll();

        void Add(ProductVariantModel productVariant);

        void Update(ProductVariantModel productVariant);
    }
}
