using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Amazon.CopyAndMedia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Amazon.CopyAndMedia
{
    public class CopywritingValidator : ICopywritingValidator
    {
        private readonly IRepository<Copywriting> _repository;

        public CopywritingValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Copywriting>();
        }

        public ValidationResult Validate(CopywritingModel copywriting)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != copywriting.Id && c.ProductId == copywriting.Product.Id &&
                c.InstanceId == copywriting.Instance.Id && c.ElementId == copywriting.Element.Id))
            {
                result.AddError("This element already exists for selected Product and Instance.");
            }

            return result;
        }
    }
}
