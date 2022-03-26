using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Keywords
{
    public class AddAdvertisingReportResearchTargetsModel
    {
        [Required]
        public ResearchModel Research { get; set; }

        public IList<AdvertisingReportResearchTargetModel> Targets { get; set; } = new List<AdvertisingReportResearchTargetModel>();
    }
}
