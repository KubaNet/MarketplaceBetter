using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Base
{
    public class EntityStatus : Entity
    {
        public string Name { get; set; }

        public EntityStatusEnum SystemName { get; set; }
    }
}
