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
    public class MainPhraseValidator : IMainPhraseValidator
    {
        private readonly IRepository<MainPhrase> _repository;

        public MainPhraseValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<MainPhrase>();
        }

        public ValidationResult Validate(MainPhraseModel phrase)
        {
            ValidationResult result = new();

            if (_repository.Any(p => p.Id != phrase.Id && p.Name == phrase.Name && p.AnalysisId == phrase.Analysis.Id))
            {
                result.AddErrorFor<MainPhraseModel>(c => c.Name, ValidationMessages.NameNotUniqueForSelected, "Main Phrase", "Analysis");
            }

            return result;
        }
    }
}
