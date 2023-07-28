using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces
{
    public interface IChildCommentService
    {
        void Add(ChildModel child);

        void Update(ChildModel child);

        void Delete(ChildModel child);
    }
}
