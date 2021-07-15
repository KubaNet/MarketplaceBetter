using MarketplaceBetter.Domain.Model.Catalog;
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

        void Add(ProductModel Product);

        void Update(ProductModel Product);
    }
}
