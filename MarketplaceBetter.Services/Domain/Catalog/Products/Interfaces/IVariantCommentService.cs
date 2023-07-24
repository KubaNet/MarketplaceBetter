using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces
{
    public interface IVariantCommentService
    {
        void Add(VariantModel variant);

        void Update(VariantModel variant);

        void Delete(VariantModel variant);
    }
}
