using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia
{
    public class PhotoUploadModel
    {
        public long Id { get; set; }

        public IList<PhotoUploadVariantModel> Variants { get; set; }

        public InstanceModel Instance { get; set; }

        public PhotoTypeModel Type { get; set; }

        public PhotoKindModel Kind { get; set; }

        public string CloudId { get; set; }

        public string Version { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
