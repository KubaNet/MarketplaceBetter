using MarketplaceBetter.Domain.Model.Catalog;
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

        void Add(SizeGroupModel group);

        void Update(SizeGroupModel group);
    }
}
