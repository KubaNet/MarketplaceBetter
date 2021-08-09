using MarketplaceBetter.Domain.Entities.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon
{
    public class MainPhraseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public MainPhraseStatusModel Status { get; set; }
    }
}
