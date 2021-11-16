using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface IColorTranslationService
    {
        ColorTranslationModel Get(long id);

        IList<ColorTranslationModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<ColorTranslationModel> GetForListRequest(ListRequest request);

        void Add(ColorTranslationModel group);

        void Update(ColorTranslationModel group);
    }
}
