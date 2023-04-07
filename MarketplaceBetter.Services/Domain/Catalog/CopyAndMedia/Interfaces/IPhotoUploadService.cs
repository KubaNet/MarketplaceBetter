using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.Products;
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

        void Remove(IList<PhotoUploadModel> photoUploads);

        void RemoveAllForVariant(long variantId);

        void SetType(IList<PhotoUploadModel> photoUploads, PhotoTypeModel type);

        void Save(PhotoUploadModel photoUpload, VariantModel variant);
    }
}
