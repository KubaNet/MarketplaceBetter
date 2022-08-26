using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Campaigns.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Campaigns
{
    public class NegativeKeywordValidator : INegativeKeywordValidator
    {
        private readonly IRepository<NegativeKeyword> _repository;

        public NegativeKeywordValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<NegativeKeyword>();
        }

        public ValidationResult Validate(NegativeKeywordModel negativeKeyword)
        {
            ValidationResult result = new();

            if (_repository.Any(k => k.Id != negativeKeyword.Id && k.AdGroupId == negativeKeyword.AdGroup.Id && k.Keyword == negativeKeyword.Keyword
                && k.MatchTypeId == negativeKeyword.MatchType.Id))
            {
                result.AddError("There already exists a Negative Keyword for this Campaign, Keyword and Match Type.");
            }

            return result;
        }
    }
}
