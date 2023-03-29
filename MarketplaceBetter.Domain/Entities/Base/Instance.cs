using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Base
{
    public class Instance : Entity
    {
        public string Name { get; set; }

        public InstanceEnum SystemName { get; set; }

        public string ShortName { get; set; }

        public bool IsNormal { get; set; }
    }
}
