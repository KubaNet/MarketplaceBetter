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
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepository<Currency> _repository;

        public CurrencyService(
            IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Currency>();
        }

        public Currency GetByName(string name) => _repository.Single(c => c.Name == name);
    }
}
