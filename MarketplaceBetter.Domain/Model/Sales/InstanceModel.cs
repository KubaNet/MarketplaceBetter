using MarketplaceBetter.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales
{
    public class InstanceModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public InstanceEnum SystemName { get; set; }

        public string ShortName { get; set; }
    }
}
