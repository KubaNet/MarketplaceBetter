using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Catalog.CopyAndMedia.Interfaces
{
    public interface IPhotoValidator
    {
        ValidationResult Validate(PhotoModel photo);

        ValidationResult ValidateTypeChange(IList<PhotoModel> photos, PhotoTypeModel type);
    }
}
