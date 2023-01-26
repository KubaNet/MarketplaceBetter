using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface IPhotoUploadService
    {
        IList<PhotoUploadModel> GetForListRequest(ListRequest request);

        void Remove(string cloudId);
    }
}
