using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Sales.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Sales.Settings
{
    public class VatRuleValidator : IVatRuleValidator
    {
        private readonly IRepository<VatRule> _repository;

        public VatRuleValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<VatRule>();
        }

        public ValidationResult Validate(VatRuleModel rule)
        {
            ValidationResult result = new();

            if (_repository.Any(r => r.Id != rule.Id && r.CountryFromId == rule.CountryFrom.Id && r.CountryToId == rule.CountryTo.Id))
            {
                result.AddError("There already exists Vat Rule for these Countries");
            }

            return result;
        }
    }
}
