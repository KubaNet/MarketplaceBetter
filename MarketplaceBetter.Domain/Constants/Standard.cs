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

        public const int InitialRowsPerPage = 50;

        public const int SmallestRowsPerPage = 50;

        public const int MediumRowsPerPage = 100;

        public const int LargestRowsPerPage = 200;

        public const long UploadPhotoSizeLimit = 20971520; //20MB

        public const long UploadTemplateSizeLimit = 20971520; //20MB
    }
}
