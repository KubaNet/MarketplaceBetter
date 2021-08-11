using MarketplaceBetter.Domain.Entities.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class KeywordDataSourceModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public KeywordDataSourceEnum SystemName { get; set; }
    }
}
