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
    public class SizeValidator : ISizeValidator
    {
        private readonly IRepository<Size> _repository;

        public SizeValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Size>();
        }

        public ValidationResult Validate(SizeModel size)
        {
            ValidationResult result = new();

            if (_repository.Any(s => s.Id != size.Id && s.Name == size.Name && s.GroupId == size.Group.Id))
            {
                result.AddErrorFor<SizeModel>(s => s.Name, ValidationMessages.NameNotUnique, "Size");
            }

            if (_repository.Any(s => s.Id != size.Id && s.Code == size.Code && s.GroupId == size.Group.Id))
            {
                result.AddErrorFor<SizeModel>(s => s.Code, ValidationMessages.PropertyNotUnique, "Size", "Code");
            }

            return result;
        }
    }
}
