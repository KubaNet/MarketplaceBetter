using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon
{
    public class KeywordResearchTargetValidator : IKeywordResearchTargetValidator
    {
        private readonly IRepository<KeywordResearchTarget> _repository;

        public KeywordResearchTargetValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<KeywordResearchTarget>();
        }

        public ValidationResult Validate(KeywordResearchTargetModel target)
        {
            ValidationResult result = new();

            if (_repository.Any(t => t.Id != target.Id && t.Name == target.Name && t.ResearchId == target.Research.Id ))
            {
                result.AddErrorFor<KeywordResearchTargetModel>(b => b.Name, ValidationMessages.NameNotUniqueForSelected, "Keyword Research Target", "Research");
            }

            return result;
        }
    }
}
