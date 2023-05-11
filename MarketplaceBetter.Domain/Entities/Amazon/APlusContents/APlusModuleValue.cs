using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusModuleValue : Entity
    {
        public APlusModuleValue()
        {
            Elements = new List<APlusElementValue>();
        }

        public long ModuleId { get; set; }

        public virtual APlusModule Module { get; set; }

        public int Order { get; set; }

		public long ContentId { get; set; }

		public virtual APlusContent Content { get; set; }

		public virtual IList<APlusElementValue> Elements { get; set; }
    }
}
