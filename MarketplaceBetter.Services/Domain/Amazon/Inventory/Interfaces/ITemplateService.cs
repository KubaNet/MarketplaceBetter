using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces
{
    public interface ITemplateService
    {
        void Save(Stream fileStream, string fileName, long parentId);

        Stream Download(long id);

        void Delete(long id);
    }
}
