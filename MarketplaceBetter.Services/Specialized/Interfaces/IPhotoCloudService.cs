using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
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

        PhotoUploadResult PreUpload(MemoryStream photoStream, string fileName);

        PhotoUploadResult SaveFromPreUpload(string cloudId, string version, string fileName);

        string GetOriginalPhotoUrl(string cloudId, string version);

        string GetPhotoUrlForLists(string cloudId, string version);

        string GetBiggerPhotoUrlForLists(string cloudId, string version);

        void Delete(string cloudId);

        void Delete(IList<string> cloudIds);
    }
}
