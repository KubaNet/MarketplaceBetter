using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface IBrandService
    {
        BrandModel Get(long id);

        IList<BrandModel> GetBrands();

        void Add(BrandModel brand);

        void Update(BrandModel brand);
    }
}
