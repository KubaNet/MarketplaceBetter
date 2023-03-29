using MarketplaceBetter.Domain.Constants;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Validation.Attributes;
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
        public ChildModel Child { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }

        [Display(Name = "SKU")]
        [Required]
        [StringLength(Standard.AmazonSkuMaxLength, MinimumLength = 2)]
        public string Sku { get; set; }
    }
}
