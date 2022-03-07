using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces
{
    public interface IColorService
    {
        ColorModel Get(long id);

        IList<ColorModel> GetAll();

        IList<ColorModel> GetAllForGroup(long groupId);

        int CountForListRequest(ListRequest request);

        IList<ColorModel> GetForListRequest(ListRequest request);

        void Add(ColorModel color);

        void Update(ColorModel color);
    }
}
