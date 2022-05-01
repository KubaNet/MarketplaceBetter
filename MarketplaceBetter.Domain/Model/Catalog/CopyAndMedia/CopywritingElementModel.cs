using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia
{
    public class CopywritingElementModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public CopywritingElementEnum SystemName { get; set; }
    }
}
