using MarketplaceBetter.Services.Model;
using System.Collections.Generic;
using System.IO;

namespace MarketplaceBetter.Specialized.Interfaces
{
    public interface IPhotoCloudService
    {
        PhotoUploadResult Upload(MemoryStream photoStream, string fileName);

        PhotoUploadResult PreUpload(MemoryStream photoStream, string fileName);

        PhotoUploadResult SaveFromPreUpload(string cloudId, string version, string fileName);

        PhotoRenamingResult Rename(string cloudId, string newFileName);

        string GetOriginalUrl(string cloudId, string version);

        string GetFormat(string cloudId);

        string GetUrlForLists(string cloudId, string version, PhotoOnListSizeEnum size);

        string GetBiggerUrlForLists(string cloudId, string version);

        void Delete(string cloudId);

        void Delete(IList<string> cloudIds);
    }
}
