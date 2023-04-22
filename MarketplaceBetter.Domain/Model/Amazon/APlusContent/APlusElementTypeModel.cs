using MarketplaceBetter.Domain.Entities.Amazon.APlusContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContent
{
    public class APlusElementTypeModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public APlusElementTypeEnum SystemName { get; set; }
    }
}
