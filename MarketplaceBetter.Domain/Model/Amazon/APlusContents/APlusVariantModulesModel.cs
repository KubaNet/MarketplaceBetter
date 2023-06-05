using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContents
{
    public class APlusVariantModulesModel
    {
        public APlusContentModel Content { get; set; }

        public InstanceModel Instance { get; set; }

        public IList<APlusModuleValueModel> Modules { get; set; }
    }
}
