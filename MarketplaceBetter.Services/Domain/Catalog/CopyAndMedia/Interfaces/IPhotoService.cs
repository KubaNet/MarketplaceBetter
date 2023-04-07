using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
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
    public interface IPhotoService
    {
        PhotoModel Get(long id);

        PhotoModel GetForVariant(long variantId, PhotoTypeEnum type);

        int CountForListRequest(ListRequest request);

        IList<PhotoModel> GetForListRequest(ListRequest request);

        void Add(PhotoModel photo);

        void Update(PhotoModel photo);

        void AddOrUpdate(PhotoModel photo, VariantModel variant);

        void Delete(long id);

        void Delete(IList<PhotoModel> photos);

        void DeleteAllForVariant(long variantId);
    }
}
