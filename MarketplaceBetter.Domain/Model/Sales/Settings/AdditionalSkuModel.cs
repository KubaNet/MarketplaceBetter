using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Settings
{
	public class AdditionalSkuModel
	{
		public long Id { get; set; }

		[Required]
		public VariantModel Variant { get; set; }

		[Display(Name = "SKU")]
		[Required]
		[StringLength(40, MinimumLength = 2)]
		public string Sku { get; set; }
	}
}
