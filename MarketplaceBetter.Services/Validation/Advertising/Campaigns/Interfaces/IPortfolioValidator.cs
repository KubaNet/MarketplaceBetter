using MarketplaceBetter.Domain.Model.Advertising.Campaigns;
using MarketplaceBetter.Domain.Validation;

namespace MarketplaceBetter.Services.Validation.Advertising.Campaigns.Interfaces
{
    public interface IPortfolioValidator
    {
        ValidationResult Validate(PortfolioModel portfolio);
    }
}
