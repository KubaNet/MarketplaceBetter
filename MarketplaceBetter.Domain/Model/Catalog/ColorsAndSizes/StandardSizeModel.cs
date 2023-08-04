using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes
{
    public class StandardSizeModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public StandardSizeEnum SystemName { get; set; }
    }
}
