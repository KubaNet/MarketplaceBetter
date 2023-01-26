using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia
{
    public class PhotoUploadModel
    {
        public IList<VariantModel> Variants { get; set; }

        public InstanceModel Instance { get; set; }

        public PhotoTypeModel Type { get; set; }

        public string CloudId { get; set; }

        public string FileName { get; set; }

        public string Url { get; set; }
    }
}
