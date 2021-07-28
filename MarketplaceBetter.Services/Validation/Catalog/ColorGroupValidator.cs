using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog
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

            if (_repository.Any(g => g.Name == group.Name && g.Id != group.Id))
            {
                result.AddErrorFor<ColorGroupModel>(g => g.Name, ValidationMessages.NameNotUnique, "Color Group");
            }

            return result;
        }
    }
}
