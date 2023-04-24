using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.APlusContents
{
	public class APlusSectionValueValidator
	{
		private readonly IRepository<APlusSectionValue> _repository;

		public APlusSectionValueValidator(IUnitOfWork unitOfWork)
		{
			_repository = unitOfWork.GetRepository<APlusSectionValue>();
		}

		public ValidationResult Validate(APlusSectionValueModel section)
		{
			ValidationResult result = new ValidationResult();

			if (_repository.Any(s => s.Id != section.Id && s.ContentId == section.Content.Id && s.Order == section.Order))
			{
				result.AddErrorFor<APlusSectionValueModel>(s => s.Order, "There already exists section for this content with such an order.");
			}

			return result;
		}
	}
}
