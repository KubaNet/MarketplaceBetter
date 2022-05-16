using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Specialized.Interfaces
{
    public interface IPhotoCloudService
    {
        PhotoUploadResult Upload(MemoryStream photoStream, string fileName);

        string GetForLists(string cloudId);
    }
}
