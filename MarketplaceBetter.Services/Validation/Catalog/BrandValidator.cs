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
    public class BrandValidator : IBrandValidator
    {
        private readonly IRepository<Brand> _repository;

        public BrandValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Brand>();
        }

        public ValidationResult Validate(BrandModel brand)
        {
            ValidationResult result = new();

            if (_repository.Any(b => b.Name == brand.Name && b.Id != brand.Id))
            {
                result.AddErrorFor<BrandModel>(b => b.Name, ValidationMessages.NameNotUnique, "Brand");
            }

            if (_repository.Any(b => b.Code == brand.Code && b.Id != brand.Id))
            {
                result.AddErrorFor<BrandModel>(b => b.Code, ValidationMessages.PropertyNotUnique, "Brand", "Code");
            }

            return result;
        }
    }
}
