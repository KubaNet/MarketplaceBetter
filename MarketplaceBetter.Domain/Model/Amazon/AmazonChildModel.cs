using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class AmazonChildModel
    {
        public long Id { get; set; }

        [Required]
        public AmazonParentModel Parent { get; set; }

        [Required]
        public ProductVariantModel ProductVariant { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Sku { get; set; }

        [BetterMaxLength]
        public string Asin { get; set; }
    }
}
