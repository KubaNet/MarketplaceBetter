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
    public class KeywordDataValidator : IKeywordDataValidator
    {
        private readonly IRepository<KeywordData> _repository;

        public KeywordDataValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<KeywordData>();
        }

        public ValidationResult Validate(KeywordDataModel data)
        {
            ValidationResult result = new();

            return result;
        }
    }
}
