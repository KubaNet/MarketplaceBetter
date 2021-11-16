using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface IColorGroupService
    {
        ColorGroupModel Get(long id);

        IList<ColorGroupModel> GetAll();

        IList<ColorGroupModel> GetForBrand(long brandId);

        int CountForListRequest(ListRequest request);

        IList<ColorGroupModel> GetForListRequest(ListRequest request);

        void Add(ColorGroupModel group);

        void Update(ColorGroupModel group);
    }
}
