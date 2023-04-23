using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public class APlusImage : Entity
    {
        public string FileName { get; set; }

        public string Path { get; set; }

        public string Keywords { get; set; }
    }
}
