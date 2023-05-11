using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.APlusContents.Interfaces
{
	public interface IAPlusModuleValueValidator
	{
		ValidationResult Validate(APlusModuleValueModel module);
	}
}
