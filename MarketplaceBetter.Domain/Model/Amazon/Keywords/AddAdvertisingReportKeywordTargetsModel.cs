using MarketplaceBetter.Domain.Model.Amazon.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Keywords
{
    public class AddAdvertisingReportKeywordTargetsModel
    {
        [Required]
        [Display(Name = "Research")]
        public KeywordResearchModel KeywordResearch { get; set; }

        public IList<AdvertisingReportKeywordTargetModel> Targets { get; set; } = new List<AdvertisingReportKeywordTargetModel>();
    }
}
