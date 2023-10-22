using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Sales.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Sales.Settings
{
	public class ProductAccountingDataValidator : IProductAccountingDataValidator
	{
		private readonly IRepository<ProductAccountingData> _repository;

		public ProductAccountingDataValidator(IUnitOfWork unitOfWork)
		{
			_repository = unitOfWork.GetRepository<ProductAccountingData>();
		}

		public ValidationResult Validate(ProductAccountingDataModel data)
		{
			ValidationResult result = new();

			if (_repository.Any(r => r.Id != data.Id && r.ProductId == data.Product.Id))
			{
				result.AddErrorFor<ProductAccountingDataModel>(d => d.Product, "There already exists Accounting Data for this Product");
			}

			if (data.Weight <= 0)
			{
                result.AddErrorFor<ProductAccountingDataModel>(d => d.Weight, "Weight has to be bigger than 0.00");
            }

			return result;
		}
	}
}
