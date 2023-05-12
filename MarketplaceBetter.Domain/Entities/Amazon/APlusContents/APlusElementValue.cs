using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusElementValue : Entity
    {
        public long ElementId { get; set; }

        public virtual APlusElement Element { get; set; }

        public string SingleLineText { get; set; }

        public string BodyText { get; set; }

        public long? ImageId { get; set; }

        public bool TrueOrFalse { get; set; }

        public virtual APlusImage Image { get; set; }
    }
}
