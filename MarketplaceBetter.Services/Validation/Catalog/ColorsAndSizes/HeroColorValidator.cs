using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.ColorsAndSizes.Interfaces;

namespace MarketplaceBetter.Services.Validation.Catalog.ColorsAndHeroColor
{
    public class HeroColorValidator : IHeroColorValidator
    {
        private readonly IRepository<HeroColor> _repository;

        public HeroColorValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<HeroColor>();
        }

        public ValidationResult Validate(HeroColorModel heroColor)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != heroColor.Id && c.ProductId == heroColor.Product.Id))
            {
                result.AddErrorFor<HeroColorModel>(s => s.Product, ValidationMessages.EntityNotUniqueForSelected, "Hero Color", "Product");
            }

            return result;
        }
    }
}
