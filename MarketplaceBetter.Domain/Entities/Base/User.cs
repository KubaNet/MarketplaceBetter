using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Base
{
    public class User : Entity
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string IpAddress { get; set; }

        public DateTime? IpLastPing { get; set; }

        public long? CurrentStatusId { get; set; }

        public virtual EntityStatus CurrentStatus { get; set; }

        public long? CurrentBrandId { get; set; }

        public virtual Brand CurrentBrand { get; set; }

        public long? CurrentCollectionId { get; set; }

        public virtual Collection CurrentCollection { get; set; }

        public long? CurrentSizeId { get; set; }

        public virtual StandardSize CurrentSize { get; set; }

        public long CurrentInstanceId { get; set; }

        public virtual Instance CurrentInstance { get; set; }

        public bool HideDrafts { get; set; }

        public bool HideWithdrawn { get; set; }

        public bool HideCopies { get; set; }

        public string ExpandedMenu { get; set; }
    }
}
