using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.Inventory
{
    public class Template : Entity
    {
        public string FileName { get; set; }

        public string Path { get; set; }
    }
}
