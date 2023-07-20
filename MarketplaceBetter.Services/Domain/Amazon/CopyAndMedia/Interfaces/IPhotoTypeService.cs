using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces
{
    public interface IPhotoTypeService
    {
        IList<PhotoTypeModel> GetAll();
    }
}
