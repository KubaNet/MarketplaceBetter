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

        int CountForListRequest(ListRequest request);

        IList<PhotoModel> GetForListRequest(ListRequest request);

        void Add(PhotoModel photo);

        void Update(PhotoModel photo);
    }
}
