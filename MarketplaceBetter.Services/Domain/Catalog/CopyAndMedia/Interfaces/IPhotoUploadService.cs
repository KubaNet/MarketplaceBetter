using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces
{
    public interface IPhotoUploadService
    {
        IList<PhotoUploadModel> GetForListRequest(ListRequest request);

        void AddOrUpdate(PhotoUploadModel photoUpload);

        void Remove(PhotoUploadModel photoUpload);
    }
}
