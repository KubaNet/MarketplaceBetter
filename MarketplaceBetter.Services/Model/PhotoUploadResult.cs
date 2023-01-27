using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Model
{
    public class PhotoUploadResult
    {
        public bool WasSuccessful { get; set; }

        public string Error { get; set; }

        public string CloudId { get; set; }

        public string Url { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
