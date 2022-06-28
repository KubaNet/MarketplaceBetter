using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Campaigns.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Campaigns
{
    public class PortfolioValidator : IPortfolioValidator
    {
        private readonly IRepository<Portfolio> _repository;

        public PortfolioValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Portfolio>();
        }

        public ValidationResult Validate(PortfolioModel portfolio)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != portfolio.Id && p.Name == portfolio.Name && p.InstanceId == portfolio.Instance.Id))
            {
                result.AddError("There already exists a Portfolio with this Name for this Instance.");
            }

            return result;
        }
    }
}
