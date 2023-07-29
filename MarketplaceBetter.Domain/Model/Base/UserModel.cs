using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Base
{
    public class UserModel
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string IpAddress { get; set; }

        public DateTime? IpLastPing { get; set; }

        public EntityStatusModel Status { get; set; }

        public BrandModel CurrentBrand { get; set; }

        public CollectionModel CurrentCollection { get; set; }

        public InstanceModel CurrentInstance { get; set; }

        public bool HideDrafts { get; set; }

        public bool HideWithdrawn { get; set; }

        public string ExpandedMenu { get; set; }
    }
}
