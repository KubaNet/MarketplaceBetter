using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceBetter.Web.ModelBuilders.Catalog
{
    public interface IBrandModelBuilder
    {
        IList<BrandModel> GetBrands();
    }
}
