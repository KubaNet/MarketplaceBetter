using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia
{
    public class PhotoTypeModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string AmazonUploadCode { get; set; }

        public PhotoTypeEnum SystemName { get; set; }
    }
}
