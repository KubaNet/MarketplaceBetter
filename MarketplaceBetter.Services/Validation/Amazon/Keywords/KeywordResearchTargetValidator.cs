using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.Keywords.Interfaces;

namespace MarketplaceBetter.Services.Validation.Amazon.Keywords
{
    public class ResearchTargetValidator : IResearchTargetValidator
    {
        private readonly IRepository<ResearchTarget> _repository;

        public ResearchTargetValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ResearchTarget>();
        }

        public ValidationResult Validate(ResearchTargetModel target)
        {
            ValidationResult result = new();

            if (_repository.Any(t => t.Id != target.Id && t.Name == target.Name && t.ResearchId == target.Research.Id ))
            {
                result.AddErrorFor<ResearchTargetModel>(b => b.Name, ValidationMessages.NameNotUniqueForSelected, "Research Target", "Research");
            }

            return result;
        }
    }
}
