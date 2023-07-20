using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
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

            if (_repository.Any(c => c.Id != photo.Id && c.VariantId == photo.Variant.Id && c.InstanceId == photo.Instance.Id && c.TypeId == photo.Type.Id))
            {
                result.AddError("A photo already exists for selected Variant, Instance and Type.");
            }

            return result;
        }

        public ValidationResult ValidateTypeChange(IList<PhotoModel> photos, PhotoTypeModel type)
        {
            ValidationResult result = new();

            foreach (PhotoModel photo in photos)
            {
                if (_repository.Any(p => p.Id != photo.Id && p.VariantId == photo.Variant.Id && p.InstanceId == photo.Instance.Id && p.TypeId == type.Id))
                {
                    result.AddError($"There already exists photo of type {type.Name} for variant {photo.Variant.Sku}.");
                }
            }

            return result;
        }
    }
}
