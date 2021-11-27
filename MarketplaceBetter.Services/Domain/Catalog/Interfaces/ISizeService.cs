using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface ISizeService
    {
        SizeModel Get(long id);

        IList<SizeModel> GetAll();

        IList<SizeModel> GetAllForGroup(long groupId);

        int CountForListRequest(ListRequest request);

        IList<SizeModel> GetForListRequest(ListRequest request);

        void Add(SizeModel color);

        void Update(SizeModel color);
    }
}
