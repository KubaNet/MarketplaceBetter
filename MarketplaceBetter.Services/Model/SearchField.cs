using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Model
{
    public class SearchField
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Value);
        }
    }
}
