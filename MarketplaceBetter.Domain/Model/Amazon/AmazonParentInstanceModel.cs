using MarketplaceBetter.Domain.Constants;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class AmazonParentInstanceModel
    {
        public long Id { get; set; }

        [Required]
        public AmazonParentModel Parent { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }

        [Display(Name = "SKU")]
        [Required]
        [StringLength(Standard.AmazonSkuMaxLength, MinimumLength = 2)]
        public string Sku { get; set; }

        [Display(Name = "ASIN")]
        [StringLength(Standard.AmazonAsinLength, MinimumLength = 0)]
        public string Asin { get; set; }
    }
}
