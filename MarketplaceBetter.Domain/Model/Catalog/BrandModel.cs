using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog
{
    public class BrandModel
    {
        public long Id { get; set; }

        [Required]
        [BetterLength]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 2)]
        public string Code { get; set; }
    }
}
