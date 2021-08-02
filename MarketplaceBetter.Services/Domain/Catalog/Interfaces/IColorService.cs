using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface IColorService
    {
        ColorModel Get(long id);

        IList<ColorModel> GetAll();

        IList<ColorModel> GetAllForGroup(long groupId);

        void Add(ColorModel color);

        void Update(ColorModel color);
    }
}
