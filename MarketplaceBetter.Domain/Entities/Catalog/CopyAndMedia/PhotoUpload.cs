using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia
{
    public class PhotoUpload : Entity
    {
        public virtual IList<PhotoUploadVariant> Variants { get; set; }

        public long? InstanceId { get; set; }

        public virtual Instance Instance { get; set; }

        public long? TypeId { get; set; }

        public virtual PhotoType Type { get; set; }

        public string CloudId { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
