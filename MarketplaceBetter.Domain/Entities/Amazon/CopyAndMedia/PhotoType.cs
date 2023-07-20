using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia
{
    public class PhotoType : Entity
    {
        public string Name { get; set; }

        public string AmazonUploadCode { get; set; }

        public PhotoTypeEnum SystemName { get; set; }
    }
}
