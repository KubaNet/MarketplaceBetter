using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces
{
    public interface ICopywritingElementService
    {
        IList<CopywritingElementModel> GetAll();
    }
}
