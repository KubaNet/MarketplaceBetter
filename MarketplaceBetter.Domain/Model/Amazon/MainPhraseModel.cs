using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class MainPhraseModel
    {
        public long Id { get; set; }

        [Required]
        public KeywordAnalysisModel Analysis { get; set; }

        [Required]
        [BetterLength]
        public string Name { get; set; }

        [BetterMaxLength]
        public string Description { get; set; }

        [Required]
        public MainPhraseStatusModel Status { get; set; }
    }
}
