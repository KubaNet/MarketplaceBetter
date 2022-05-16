using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Validation;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Validation.Catalog.CopyAndMedia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.CopyAndMedia
{
    public class PhotoValidator : IPhotoValidator
    {
        private readonly IRepository<Photo> _repository;

        public PhotoValidator(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Photo>();
        }

        public ValidationResult Validate(PhotoModel photo)
        {
            ValidationResult result = new();

            if (photo.Instance != null)
            {
                if (_repository.Any(c => c.Id != photo.Id && c.VariantId == photo.Variant.Id &&
                    c.InstanceId == photo.Instance.Id && c.TypeId == photo.Type.Id))
                {
                    result.AddError("A photo already exists for selected Variant, Instance and Type.");
                }
            }
            else
            {
                if (_repository.Any(c => c.Id != photo.Id && c.VariantId == photo.Variant.Id &&
                    c.InstanceId == null && c.TypeId == photo.Type.Id))
                {
                    result.AddError("A photo already exists for selected Variant and Type.");
                }
            }

            return result;
        }
    }
}
