using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContents
{
    public class APlusElementValueModel
    {
        public long Id { get; set; }

        public APlusElementModel Element { get; set; }

        public string SingleLineText { get; set; }

        public string MultiLineText { get; set; }

        public APlusImageModel Image { get; set; }
    }
}
