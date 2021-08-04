using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales
{
    public class SalesChannelModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SalesChannelEnum SystemName { get; set; }
    }
}
