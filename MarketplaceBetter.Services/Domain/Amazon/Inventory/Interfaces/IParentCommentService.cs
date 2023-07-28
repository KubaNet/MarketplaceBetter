using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces
{
    public interface IParentCommentService
    {
        void Add(ParentModel parent);

        void Update(ParentModel parent);

        void Delete(ParentModel parent);
    }
}
