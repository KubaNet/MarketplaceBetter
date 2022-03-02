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
    public class KeywordResearchValidator : IKeywordResearchValidator
    {
        private readonly IRepository<KeywordResearch> _repository;

        public KeywordResearchValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<KeywordResearch>();
        }

        public ValidationResult Validate(KeywordResearchModel research)
        {
            ValidationResult result = new();

            if (_repository.Any(r => r.Id != research.Id && r.Name == research.Name))
            {
                result.AddErrorFor<KeywordResearchModel>(b => b.Name, ValidationMessages.NameNotUnique, "Keyword Research");
            }

            return result;
        }
    }
}
