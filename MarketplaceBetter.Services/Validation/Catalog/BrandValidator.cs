using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
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
    public class ResearchValidator : IBrandValidator
    {
        private readonly IRepository<Brand> _repository;

        public ResearchValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Brand>();
        }

        public ValidationResult Validate(BrandModel brand)
        {
            ValidationResult result = new();

            if (_repository.Any(b => b.Id != brand.Id && b.Name == brand.Name))
            {
                result.AddErrorFor<BrandModel>(b => b.Name, ValidationMessages.NameNotUnique, "Brand");
            }

            if (_repository.Any(b => b.Id != brand.Id && b.Code == brand.Code))
            {
                result.AddErrorFor<BrandModel>(b => b.Code, ValidationMessages.PropertyNotUnique, "Brand", "Code");
            }

            return result;
        }
    }
}
