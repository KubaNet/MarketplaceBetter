using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces
{
    public interface IProductCommentService
    {
        void Add(ProductModel product);

        void Update(ProductModel product);

        void Delete(ProductModel product);
    }
}
