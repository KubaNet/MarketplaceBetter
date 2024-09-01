using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Sales.Settings
{
	public class VatRuleModel
	{
		public long Id { get; set; }

		[Display(Name = "Country From")]
		[Required]
		public CountryModel CountryFrom { get; set; }

		[Display(Name = "Country To")]
		[Required]
		public CountryModel CountryTo { get; set; }

		[Display(Name = "Vat Value")]
		[Required]
		[Range(0, 30)]
		public double VatValue { get; set; }

		[Display(Name = "Vat Number")]
		[Required]
		[BetterLength]
		public string VatNumber { get; set; }
	}
}
