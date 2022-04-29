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
    public class ColorGroupValidator : IColorGroupValidator
    {
        private readonly IRepository<ColorGroup> _repository;

        public ColorGroupValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ColorGroup>();
        }

        public ValidationResult Validate(ColorGroupModel group)
        {
            ValidationResult result = new();

            if (_repository.Any(g => g.Id != group.Id && g.BrandId == group.Brand.Id && g.Name == group.Name))
            {
                result.AddErrorFor<ColorGroupModel>(g => g.Name, ValidationMessages.NameNotUniqueForSelected, "Color Group", "Brand");
            }

            return result;
        }
    }
}
