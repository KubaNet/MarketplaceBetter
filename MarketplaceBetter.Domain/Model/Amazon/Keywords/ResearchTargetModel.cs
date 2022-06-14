using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Keywords
{
    public class ResearchTargetModel
    {
        public long Id { get; set; }

        [Required]
        [BetterLength]
        public string Name { get; set; }

        [Required]
        public ResearchModel Research { get; set; }

        public int? Helium10Value { get; set; }

        public int? AmazonSearchTermsValue { get; set; }

        public ResearchTargetStatusModel Status { get; set; }
    }
}
