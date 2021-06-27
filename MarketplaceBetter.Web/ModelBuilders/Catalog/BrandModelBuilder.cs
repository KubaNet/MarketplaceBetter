using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceBetter.Web.ModelBuilders.Catalog
{
    public class BrandModelBuilder : IBrandModelBuilder
    {
        public IList<BrandModel> GetBrands()
        {
            IList<BrandModel> brands = new List<BrandModel> { new BrandModel { Id = 1, Name = "Futrzane", Code = "FUT" } };

            return brands;
        }
    }
}
