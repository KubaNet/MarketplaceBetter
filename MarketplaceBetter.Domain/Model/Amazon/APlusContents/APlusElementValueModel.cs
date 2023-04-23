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

        public string SingleLineTextValue { get; set; }

        public string MultipleLineTextValue { get; set; }

        public APlusImageModel Image { get; set; }
    }
}
