using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces
{
    public interface IColorGroupService
    {
        ColorGroupModel Get(long id);

        IList<ColorGroupModel> GetAll();

        IList<ColorGroupModel> GetAllForBrand(long brandId);

        int CountForListRequest(ListRequest request);

        IList<ColorGroupModel> GetForListRequest(ListRequest request);

        void Add(ColorGroupModel group);

        void Update(ColorGroupModel group);
    }
}
