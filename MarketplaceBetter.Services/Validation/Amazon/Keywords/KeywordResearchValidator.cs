using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Keywords.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.Keywords
{
    public class ResearchValidator : IResearchValidator
    {
        private readonly IRepository<Research> _repository;

        public ResearchValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Research>();
        }

        public ValidationResult Validate(ResearchModel research)
        {
            ValidationResult result = new();

            if (_repository.Any(r => r.Id != research.Id && r.Name == research.Name))
            {
                result.AddErrorFor<ResearchModel>(b => b.Name, ValidationMessages.NameNotUnique, "Research");
            }

            return result;
        }
    }
}
