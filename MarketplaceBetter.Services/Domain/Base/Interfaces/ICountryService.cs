using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base.Interfaces
{
    public interface ICountryService
    {
        bool Exists(string code);

        Country GetByCode(string code);

        IList<CountryModel> GetAll();
    }
}
