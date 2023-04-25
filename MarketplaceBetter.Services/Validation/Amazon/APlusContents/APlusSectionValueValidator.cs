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
	public class APlusSectionValueValidator : IAPlusSectionValueValidator
    {
		private readonly IRepository<APlusSectionValue> _repository;

		public APlusSectionValueValidator(IUnitOfWork unitOfWork)
		{
			_repository = unitOfWork.GetRepository<APlusSectionValue>();
		}

		public ValidationResult Validate(APlusSectionValueModel sectionValue)
		{
			ValidationResult result = new ValidationResult();

			if (_repository.Any(s => s.Id != sectionValue.Id && s.ContentId == sectionValue.Content.Id && s.Order == sectionValue.Order))
			{
				result.AddErrorFor<APlusSectionValueModel>(s => s.Order, "There already exists section for this content with such an order.");
			}

			return result;
		}
	}
}
