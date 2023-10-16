using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Settings
{
    public class FulfillmentCenterModel
    {
        public long Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string Code { get; set; }

        [Required]
        public CountryModel Country { get; set; }
    }
}
