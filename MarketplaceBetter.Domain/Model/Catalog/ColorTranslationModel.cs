using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog
{
    public class ColorTranslationModel
    {
        public long Id { get; set; }

        [Required]
        public InstanceModel Instance { get; set; }

        [Required]
        public ColorModel Color { get; set; }

        [Required]
        [BetterLength]
        public string Translation { get; set; }

        [BetterMaxLength]
        public string Mapping { get; set; }
    }
}