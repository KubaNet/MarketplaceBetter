using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.Products
{
    public class CollectionValidator : ICollectionValidator
    {
        private readonly IRepository<Collection> _repository;

        public CollectionValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Collection>();
        }

        public ValidationResult Validate(CollectionModel collection)
        {
            ValidationResult result = new();

            if (_repository.Any(c => c.Id != collection.Id && c.Name == collection.Name && c.BrandId == collection.Brand.Id))
            {
                result.AddErrorFor<CollectionModel>(c => c.Name, ValidationMessages.NameNotUniqueForSelected, "Collection", "Brand");
            }

            return result;
        }
    }
}
