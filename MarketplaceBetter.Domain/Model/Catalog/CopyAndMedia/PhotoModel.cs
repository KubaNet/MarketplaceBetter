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
    public class PhotoModel
    {
        public long Id { get; set; }

        [Required]
        public VariantModel Variant { get; set; }

        public bool ForAllInstances { get; set; }

        public InstanceModel Instance { get; set; }

        [Required]
        public PhotoTypeModel Type { get; set; }

        public string CloudId { get; set; }

        public string Url { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
