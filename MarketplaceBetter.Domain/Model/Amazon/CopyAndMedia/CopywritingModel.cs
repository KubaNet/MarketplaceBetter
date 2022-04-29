using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia
{
    public class CopywritingModel
    {
        public long Id { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }

        [Required]
        public CopywritingElementModel Element { get; set; }

        [Required]
        [StringLength(4000, MinimumLength = 2)]
        public string Value { get; set; }
    }
}
