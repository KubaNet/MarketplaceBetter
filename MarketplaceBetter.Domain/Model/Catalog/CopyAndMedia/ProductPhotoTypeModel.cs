using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia
{
    public class ProductPhotoTypeModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ProductPhotoTypeEnum SystemName { get; set; }
    }
}
