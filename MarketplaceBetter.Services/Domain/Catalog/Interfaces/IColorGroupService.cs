using MarketplaceBetter.Domain.Model.Catalog;
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

        void Add(ColorGroupModel group);

        void Update(ColorGroupModel group);
    }
}
