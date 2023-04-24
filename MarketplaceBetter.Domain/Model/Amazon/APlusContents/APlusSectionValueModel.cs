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

        public APlusSectionModel Section { get; set; }

        public int Order { get; set; }

        public APlusContentModel Content { get; set; }

        public IList<APlusElementValueModel> Elements { get; set; }
    }
}
