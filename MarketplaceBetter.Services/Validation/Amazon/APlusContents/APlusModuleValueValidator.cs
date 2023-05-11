using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.APlusContents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.APlusContents
{
	public class APlusModuleValueValidator : IAPlusModuleValueValidator
    {
		private readonly IRepository<APlusModuleValue> _repository;

		public APlusModuleValueValidator(IUnitOfWork unitOfWork)
		{
			_repository = unitOfWork.GetRepository<APlusModuleValue>();
		}

		public ValidationResult Validate(APlusModuleValueModel moduleValue)
		{
			ValidationResult result = new ValidationResult();

			if (_repository.Any(s => s.Id != moduleValue.Id && s.ContentId == moduleValue.Content.Id && s.Order == moduleValue.Order))
			{
				result.AddErrorFor<APlusModuleValueModel>(s => s.Order, "There already exists module for this content with such an order.");
			}

			return result;
		}
	}
}
