using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _repository;

        public CountryService(
            IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Country>();
        }

        public Country GetByCode(string code) => _repository.Single(c => c.Code == code);
    }
}
