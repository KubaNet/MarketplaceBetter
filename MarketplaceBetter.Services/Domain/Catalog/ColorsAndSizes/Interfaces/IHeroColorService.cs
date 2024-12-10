using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Services.Model;
using System.Collections.Generic;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces
{
    public interface IHeroColorService
    {
        HeroColorModel Get(long id);

        int CountForListRequest(ListRequest request);

        IList<HeroColorModel> GetForListRequest(ListRequest request);

        void Add(HeroColorModel heroColor);

        void Update(HeroColorModel heroColor);
    }
}
