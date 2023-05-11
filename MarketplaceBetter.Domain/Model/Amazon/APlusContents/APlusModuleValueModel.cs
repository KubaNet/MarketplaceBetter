using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContents
{
    public class APlusModuleValueModel
    {
        public APlusModuleValueModel()
        {
            Elements = new List<APlusElementValueModel>();
        }

        public long Id { get; set; }

        [Required]
        public APlusModuleModel Module { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public APlusContentModel Content { get; set; }

        public IList<APlusElementValueModel> Elements { get; set; }
    }
}
