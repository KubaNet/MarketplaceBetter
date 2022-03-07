using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.Keywords
{
    public class AdvertisingReportKeywordTargetModel
    {
        public string Campaign { get; set; }

        public string SearchTerm { get; set; }

        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return $"{Campaign} - {SearchTerm}";
        }
    }
}
