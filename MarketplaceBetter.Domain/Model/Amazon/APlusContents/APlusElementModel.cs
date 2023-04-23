using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContents
{
    public class APlusElementModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public APlusElementTypeModel Type { get; set; }
    }
}
