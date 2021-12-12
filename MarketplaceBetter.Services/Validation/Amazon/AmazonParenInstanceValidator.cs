using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
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
    public class AmazonParentInstanceValidator : IAmazonParentInstanceValidator
    {
        private readonly IRepository<AmazonParentInstance> _repository;

        public AmazonParentInstanceValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<AmazonParentInstance>();
        }

        public ValidationResult Validate(AmazonParentInstanceModel ParentInstance)
        {
            ValidationResult result = new();

            return result;
        }
    }
}
