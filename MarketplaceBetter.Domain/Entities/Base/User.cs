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

        public long CurrentBrandId { get; set; }

        public virtual Brand CurrentBrand { get; set; }

        public long CurrentInstanceId { get; set; }

        public virtual Instance CurrentInstance { get; set; }

        public bool ShowDrafts { get; set; }

        public bool ShowWithdrawn { get; set; }

        public string ExpandedMenu { get; set; }
    }
}
