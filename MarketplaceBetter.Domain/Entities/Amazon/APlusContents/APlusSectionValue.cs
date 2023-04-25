using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusSectionValue : Entity
    {
        public APlusSectionValue()
        {
            Elements = new List<APlusElementValue>();
        }

        public long SectionId { get; set; }

        public virtual APlusSection Section { get; set; }

        public int Order { get; set; }

		public long ContentId { get; set; }

		public virtual APlusContent Content { get; set; }

		public virtual IList<APlusElementValue> Elements { get; set; }
    }
}
