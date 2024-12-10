using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Validation;

namespace MarketplaceBetter.Services.Validation.Catalog.ColorsAndSizes.Interfaces
{
    public interface IHeroColorValidator
    {
        ValidationResult Validate(HeroColorModel heroColor);
    }
}
