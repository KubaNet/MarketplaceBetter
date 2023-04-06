using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.Products
{
    public class ProductModel
    {
        public long Id { get; set; }

        [Required]
        [BetterLength]
        public string Name { get; set; }

        [Required]
        [BetterLength]
        public string Code { get; set; }

        [Required]
        public BrandModel Brand { get; set; }

        [Required]
        public CollectionModel Collection { get; set; }

        [Display(Name = "Color group")]
        [Required]
        public ColorGroupModel ColorGroup { get; set; }

        [Display(Name = "Size group")]
        [Required]
        public SizeGroupModel SizeGroup { get; set; }

        [Required]
        public int Order { get; set; }

        public EntityStatusModel Status { get; set; }
    }
}