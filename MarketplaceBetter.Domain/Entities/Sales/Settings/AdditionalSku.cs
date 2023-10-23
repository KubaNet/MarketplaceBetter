using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.Settings
{
	public class AdditionalSku : Entity
	{
		public long VariantId { get; set; }

		public virtual Variant Variant { get; set; }

		public string Sku { get; set; }
	}
}
