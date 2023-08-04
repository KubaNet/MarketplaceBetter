using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes
{
    public class StandardSizeService : IStandardSizeService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<StandardSize> _repository;

        public StandardSizeService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<StandardSize>();
        }

        public IList<StandardSizeModel> GetAll() => _mapper.Map<IList<StandardSizeModel>>(_repository.GetQuery().OrderBy(s => s.Id));
    }
}
