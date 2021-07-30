using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog
{
    public class SizeGroupValidator : ISizeGroupValidator
    {
        private readonly IRepository<SizeGroup> _repository;

        public SizeGroupValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<SizeGroup>();
        }

        public ValidationResult Validate(SizeGroupModel group)
        {
            ValidationResult result = new();

            if (_repository.Any(g => g.Name == group.Name && g.Id != group.Id))
            {
                result.AddErrorFor<SizeGroupModel>(g => g.Name, ValidationMessages.NameNotUnique, "Size Group");
            }

            return result;
        }
    }
}
