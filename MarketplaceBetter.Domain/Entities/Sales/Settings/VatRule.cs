using MarketplaceBetter.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Sales.Settings
{
	public class VatRule : Entity
	{
		public long CountryFromId { get; set; }

		public virtual Country CountryFrom {  get; set; }

		public long CountryToId { get; set; }

		public virtual Country CountryTo { get; set; }

		public int VatValue { get; set; }

		public string VatNumber { get; set; }
	}
}
