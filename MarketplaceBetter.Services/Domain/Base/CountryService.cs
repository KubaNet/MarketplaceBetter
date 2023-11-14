using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
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
        private readonly IMapper _mapper;
        private readonly IRepository<Country> _repository;

        public CountryService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<Country>();
        }

        public bool Exists(string code) => _repository.Any(c => c.Code == code);

        public Country GetByCode(string code) => _repository.Single(c => c.Code == code);

        public IList<CountryModel> GetAll() => _mapper.Map<IList<CountryModel>>(_repository.GetQuery().OrderBy(c => c.Name));
    }
}
