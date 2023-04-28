using MarketplaceBetter.Domain.Constants;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Inventory
{
    public class ParentInstanceModel
    {
        public long Id { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }

        [Display(Name = "SKU")]
        [Required]
        [StringLength(Standard.AmazonSkuMaxLength, MinimumLength = 2)]
        public string Sku { get; set; }

        [Display(Name = "ASIN")]
        [StringLength(Standard.AmazonAsinLength, MinimumLength = 0)]
        public string Asin { get; set; }

        public EntityStatusModel Status { get; set; }

        public TemplateModel Template { get; set; }

		[BetterLength]
		public string Category { get; set; }
    }
}
