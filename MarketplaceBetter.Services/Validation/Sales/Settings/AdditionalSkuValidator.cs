using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Sales.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Sales.Settings
{
	public class AdditionalSkuValidator : IAdditionalSkuValidator
	{
		private readonly IRepository<AdditionalSku> _repository;

        public AdditionalSkuValidator(
			IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AdditionalSku>();
        }

        public ValidationResult Validate(AdditionalSkuModel sku)
		{
			ValidationResult result = new();

			if (_repository.Any(s => s.Id != sku.Id && s.VariantId == sku.Variant.Id && s.Sku.Equals(sku.Sku)))
			{
				result.AddErrorFor<AdditionalSkuModel>(s => s.Sku, ValidationMessages.PropertyNotUnique, "Variant", "Additional Sku");
			}

			return result;
		}
	}
}
