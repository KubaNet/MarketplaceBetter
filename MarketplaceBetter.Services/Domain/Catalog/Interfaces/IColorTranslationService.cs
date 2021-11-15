using MarketplaceBetter.Domain.Model.Catalog;
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

        void Add(ColorTranslationModel group);

        void Update(ColorTranslationModel group);
    }
}
