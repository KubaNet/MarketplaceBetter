using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface ISizeGroupService
    {
        SizeGroupModel Get(long id);

        IList<SizeGroupModel> GetAll();

        int CountForListRequest(ListRequest request);

        IList<SizeGroupModel> GetForListRequest(ListRequest request);

        void Add(SizeGroupModel group);

        void Update(SizeGroupModel group);
    }
}
