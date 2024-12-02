using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces
{
    public interface IPhotoService
    {
        PhotoModel Get(long id);

        PhotoModel GetForVariant(long variantId, PhotoTypeEnum type, InstanceEnum instance);

        IList<PhotoModel> GetForVariantAndInstanceAll(long variantId);

        int CountForListRequest(ListRequest request, bool showSharedOnly);

        IList<PhotoModel> GetForListRequest(ListRequest request, bool showSharedOnly);

        void Add(PhotoModel photo);

        void Update(PhotoModel photo);

        void AddOrUpdate(PhotoModel photo, VariantModel variant);

        void Delete(long id);

        void Delete(IList<PhotoModel> photos);

        void DeleteAllForVariant(long variantId);

        void UpdateType(IList<PhotoModel> photos, PhotoTypeModel type);

        Task PrepareForDownload(PhotoModel photo, VariantModel variant);

        Stream DownloadPhotos();

        void ClearPhotosToDownload();
    }
}
