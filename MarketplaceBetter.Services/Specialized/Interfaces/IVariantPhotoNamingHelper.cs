using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface IVariantPhotoNamingHelper
    {
        IList<Variant> GetVariantsFrom(string fileName);

        Instance GetInstanceFrom(string fileName);

        PhotoKind GetPhotoKindFrom(string fileName);
    }
}
