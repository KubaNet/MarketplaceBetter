using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.ColorsAndSizes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.ColorsAndSizes
{
    public class ColorTranslationValidator : IColorTranslationValidator
    {
        private readonly IRepository<ColorTranslation> _repository;

        public ColorTranslationValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ColorTranslation>();
        }

        public ValidationResult Validate(ColorTranslationModel color)
        {
            ValidationResult result = new();

            if (_repository.Any(t => t.Id != color.Id && t.InstanceId == color.Instance.Id && t.ColorId == color.Color.Id))
            {
                result.AddErrorFor<ColorTranslationModel>(t => t.Color, "There already exists translation of this Color for selected Instance.");
            }

            return result;
        }
    }
}
