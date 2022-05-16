using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Constants
{
    public static class Standard
    {
        public const int StringMinLength = 2;

        public const int StringMaxLength = 255;

        public const int AmazonSkuMaxLength = 40;

        public const int AmazonAsinLength = 10;

        public const long UploadPhotoSizeLimit = 20971520; //20MB
    }
}
