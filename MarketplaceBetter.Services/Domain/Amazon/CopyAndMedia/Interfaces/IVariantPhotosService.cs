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
    public interface IVariantPhotosService
    {
        int CountForListRequest(ListRequest request, bool showSharedOnly);

        IList<VariantPhotosModel> GetForListRequest(ListRequest request, bool showSharedOnly);

        Task PrepareForDownload(PhotoModel photo, VariantModel variant);

        Stream DownloadPhotos();

        void ClearPhotosToDownload();
    }
}
