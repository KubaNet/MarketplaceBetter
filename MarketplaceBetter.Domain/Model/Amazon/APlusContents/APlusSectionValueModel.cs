using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContents
{
    public class APlusSectionValueModel
    {
        public long Id { get; set; }

        public APlusSection Section { get; set; }

        public IList<APlusElementValueModel> Elements { get; set; }
    }
}
