using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Keywords.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Keywords
{
    public class KeywordResearchTargetValidator : IKeywordResearchTargetValidator
    {
        private readonly IRepository<KeywordTarget> _repository;

        public KeywordResearchTargetValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<KeywordTarget>();
        }

        public ValidationResult Validate(KeywordTargetModel target)
        {
            ValidationResult result = new();

            if (_repository.Any(t => t.Id != target.Id && t.Name == target.Name && t.ResearchId == target.Research.Id ))
            {
                result.AddErrorFor<KeywordTargetModel>(b => b.Name, ValidationMessages.NameNotUniqueForSelected, "Keyword Research Target", "Research");
            }

            return result;
        }
    }
}
