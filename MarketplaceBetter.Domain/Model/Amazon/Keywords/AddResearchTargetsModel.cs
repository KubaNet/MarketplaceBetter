using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Keywords
{
    public class AddResearchTargetsModel
    {
        [Required]
        public ResearchModel Research { get; set; }

        [Required]
        public ResearchTargetSourceModel Source { get; set; }
    }
}
