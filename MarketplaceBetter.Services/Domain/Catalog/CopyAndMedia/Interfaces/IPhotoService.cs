using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
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

        IList<PhotoModel> GetFromCloud();

        void Add(PhotoModel photo);

        void Update(PhotoModel photo);

        void Delete(long id);
    }
}
