using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceBetter.Domain.Model.Catalog.Products;

namespace MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia
{
    public class PhotoUploadVariantModel
    {
        public long Id { get; set; }

        public PhotoUploadModel PhotoUpload { get; set; }

        public VariantModel Variant { get; set; }
    }
}
