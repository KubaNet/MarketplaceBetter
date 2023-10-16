using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.Attributes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Sales.Settings
{
    public class FulfillmentCenterValidator : IFulfillmentCenterValidator
    {
        private readonly IRepository<FulfillmentCenter> _repository;

        public FulfillmentCenterValidator(IUnitOfWork unitOfWork)
        {
              _repository = unitOfWork.GetRepository<FulfillmentCenter>();
        }

        public ValidationResult Validate(FulfillmentCenterModel center)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != center.Id && c.Code == center.Code))
            {
                result.AddErrorFor<FulfillmentCenterModel>(c => c.Code, ValidationMessages.PropertyNotUnique, "Fulfillment Center", "Code");
            }

            return result;
        }
    }
}
