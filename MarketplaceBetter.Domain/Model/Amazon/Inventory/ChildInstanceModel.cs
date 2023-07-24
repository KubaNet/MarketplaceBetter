using MarketplaceBetter.Domain.Constants;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Inventory
{
    public class ChildInstanceModel
    {
        public long Id { get; set; }

        [Required]
        public VariantModel Variant { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }

        [Display(Name = "SKU")]
        [Required]
        [StringLength(Standard.AmazonSkuMaxLength, MinimumLength = 2)]
        public string Sku { get; set; }

        public EntityStatusModel Status { get; set; }

        public string Comment { get; set; }
    }
}
