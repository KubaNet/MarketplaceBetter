using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Amazon.APlusContents
{
    public class APlusContentModel
    {
        public APlusContentModel()
        {
            Sections = new List<APlusSectionValueModel>();
            Variants = new List<APlusContentVariantModel>();
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

		[Required]
		public ProductModel Product { get; set; }

		[Required]
		public InstanceModel Instance { get; set; }

        public EntityStatusModel Status { get; set; }

        public IList<APlusSectionValueModel> Sections { get; set; }

        public IList<APlusContentVariantModel> Variants { get; set; }
    }
}
