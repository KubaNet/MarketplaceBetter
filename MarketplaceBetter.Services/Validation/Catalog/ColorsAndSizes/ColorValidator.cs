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
    public class ColorValidator : IColorValidator
    {
        private readonly IRepository<Color> _repository;

        public ColorValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Color>();
        }

        public ValidationResult Validate(ColorModel color)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != color.Id && c.Name == color.Name && c.GroupId == color.Group.Id))
            {
                result.AddErrorFor<ColorModel>(c => c.Name, ValidationMessages.NameNotUniqueForSelected, "Color", "Group");
            }

            if (_repository.Any(c => c.Id != color.Id && c.Code == color.Code && c.GroupId == color.Group.Id))
            {
                result.AddErrorFor<ColorModel>(c => c.Code, ValidationMessages.PropertyNotUniqueForSelected, "Color", "Code", "Group");
            }

            return result;
        }
    }
}
