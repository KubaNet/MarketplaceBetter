using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Domain.Model.Catalog;
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
    public class KeywordAnalysisValidator : IKeywordAnalysisValidator
    {
        private readonly IRepository<KeywordAnalysis> _repository;

        public KeywordAnalysisValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<KeywordAnalysis>();
        }

        public ValidationResult Validate(KeywordAnalysisModel analysis)
        {
            ValidationResult result = new();

            if (_repository.Any(a => a.Id != analysis.Id && a.Name == analysis.Name))
            {
                result.AddErrorFor<BrandModel>(a => a.Name, ValidationMessages.NameNotUnique, "Keyword Analysis");
            }

            return result;
        }
    }
}
