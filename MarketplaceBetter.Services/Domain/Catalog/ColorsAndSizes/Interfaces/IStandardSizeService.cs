using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces
{
    public interface IStandardSizeService
    {
        IList<StandardSizeModel> GetAll();
    }
}
