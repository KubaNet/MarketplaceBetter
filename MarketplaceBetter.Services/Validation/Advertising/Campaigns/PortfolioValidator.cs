using MarketplaceBetter.Domain.Entities.Advertising.Campaigns;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Advertising.Campaigns;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Advertising.Campaigns.Interfaces;
using System;

namespace MarketplaceBetter.Services.Validation.Advertising.Campaigns
{
    public class PortfolioValidator : IPortfolioValidator
    {
        private readonly IRepository<Portfolio> _repository;

        public PortfolioValidator(
            IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Portfolio>();
        }
        public ValidationResult Validate(PortfolioModel portfolio)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != portfolio.Id && p.Name == portfolio.Name && p.InstanceId == portfolio.Instance.Id))
            {
                result.AddErrorFor<PortfolioModel>(p => p.Name, ValidationMessages.PropertyNotUniqueForSelected, "Portfolio", "Name", "Instance");
            }

            if (_repository.Any(p => p.Id != portfolio.Id && p.AmazonId == portfolio.AmazonId))
            {
                result.AddErrorFor<PortfolioModel>(p => p.AmazonId, ValidationMessages.PropertyNotUnique, "Portfolio", "Amazon Id");
            }

            return result;
        }
    }
}
