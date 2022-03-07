using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces
{
    public interface IBrandService
    {
        BrandModel Get(long id);

        IList<BrandModel> GetAll();

        void Add(BrandModel brand);

        void Update(BrandModel brand);
    }
}
