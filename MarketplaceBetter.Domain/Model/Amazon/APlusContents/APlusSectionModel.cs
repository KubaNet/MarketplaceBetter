using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContents
{
    public class APlusSectionModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public APlusSectionEnum SystemName { get; set; }

        public IList<APlusElementModel> Elements { get; set; }
    }
}
