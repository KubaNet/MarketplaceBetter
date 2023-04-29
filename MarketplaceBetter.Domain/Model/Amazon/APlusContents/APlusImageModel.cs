using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContents
{
    public class APlusImageModel
    {
        public long Id { get; set; }

        public string CloudId { get; set; }

        public string Version { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }
    }
}
