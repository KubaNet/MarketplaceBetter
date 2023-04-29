using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusImage : Entity
    {
        public string CloudId { get; set; }

        public string Version { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }
    }
}
